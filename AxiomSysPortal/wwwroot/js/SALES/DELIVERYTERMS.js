const modalEl = document.getElementById("upsertModal");
const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
const form = document.getElementById("termForm");
const formAlert = document.getElementById("formAlert");
const isAr = document.getElementById("culture").value === "ar";

const token = () =>
  document.querySelector('#termForm input[name="__RequestVerificationToken"]')
    .value;

function setMode(isEdit) {
  document.getElementById("formMode").value = isEdit ? "false" : "true";
  document.getElementById("modalTitle").textContent = isEdit
    ? modalEl.dataset.titleEdit
    : modalEl.dataset.titleAdd;
  document.getElementById("modalIcon").textContent = isEdit ? "✎" : "+";
  modalEl.querySelector(".modal-header").classList.toggle("edit-mode", isEdit);
}

function showError(msg) {
  formAlert.textContent = msg;
  formAlert.classList.remove("d-none");
}

function openAddModal() {
  form.reset();
  form.classList.remove("was-validated");
  formAlert.classList.add("d-none");
  document.getElementById("formCode").value = 0;
  document.getElementById("formActive").checked = true;
  setMode(false);
  modal.show();
}

async function openEditModal(code) {
  const res = await fetch(`?handler=Term&code=${code}`);
  const json = await res.json();
  if (!json.status || !json.data) {
    if (isAr) alert(json.messageAr || "لم يتم العثور على العنصر");
    else alert(json.messageEn || "Not found");
    return;
  }

  const t = json.data;
  form.classList.remove("was-validated");
  formAlert.classList.add("d-none");
  document.getElementById("formCode").value = t.code;
  document.getElementById("formSName").value = t.sName;
  document.getElementById("formBName").value = t.bName;
  document.getElementById("formDays").value = t.days ?? "";
  document.getElementById("formActive").checked = t.active;
  document.getElementById("formCode").disabled = true;
  setMode(true);
  modal.show();
}

async function saveTerm(e) {
  e.preventDefault();
  if (!form.checkValidity()) {
    form.classList.add("was-validated");
    return;
  }

  const btn = document.getElementById("btnSave");
  const spinner = document.getElementById("saveSpinner");
  btn.disabled = true;
  spinner.classList.remove("d-none");
  formAlert.classList.add("d-none");

  const daysVal = document.getElementById("formDays").value;
  const payload = {
    code: parseInt(document.getElementById("formCode").value) || 0,
    sName: document.getElementById("formSName").value.trim(),
    bName: document.getElementById("formBName").value.trim(),
    days: daysVal === "" ? null : parseInt(daysVal),
    active: document.getElementById("formActive").checked,
  };
  let action =
    document.getElementById("formMode").value === "true" ? "Create" : "Update";
  try {
    const res = await fetch(`?handler=${action}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        RequestVerificationToken: token(),
      },
      body: JSON.stringify(payload),
    });
    const json = await res.json();
    if (json.status) {
      modal.hide();
      location.reload();
    } else showError(isAr ? json.messageAr : json.messageEn || "Save failed");
  } catch {
    showError("Network error");
  } finally {
    btn.disabled = false;
    spinner.classList.add("d-none");
  }
}

async function deleteTerm(code) {
  if (isAr) {
    if (!confirm("هل أنت متأكد من الحذف")) return;
  } else {
    if (!confirm("Delete this item?")) return;
  }
  const res = await fetch(`?handler=Delete&code=${code}`, {
    method: "POST",
    headers: { RequestVerificationToken: token() },
  });
  const json = await res.json();
  if (json.status) location.reload();
  else if (isAr) alert(json.messageAr || "حدث خطأ فى الحذف");
  else alert(json.messageEn || "Delete failed");
}
