using AxiomSysPortal.Models;
using AxiomSysPortal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AxiomSysPortal.Pages.SALES.DELIVERYTERMS
{
    [Authorize]
    public class IndexModel : PageModel

    {
        private readonly SaDeliveryTermService _service;
        public IndexModel(SaDeliveryTermService service) => _service = service;

        public List<SaDeliveryTermModel> SaDeliveryTermModels { get; set; } = new();

        public async Task OnGetAsync()
        {
            var response = await _service.GetAllAsync();
            SaDeliveryTermModels = response.data!;
        }
        public async Task<IActionResult> OnGetTermAsync(short code)
        {
            var res = await _service.GetByCodeAsync(code);
            return new JsonResult(res);
        }

        public async Task<IActionResult> OnPostCreateAsync([FromBody] SaDeliveryTermModel model)
        {
            return new JsonResult(await _service.CreateAsync(model));
        }
        public async Task<IActionResult> OnPostUpdateAsync([FromBody] SaDeliveryTermModel model)
        {
            return new JsonResult(await _service.UpdateAsync(model));
        }

        public async Task<IActionResult> OnPostDeleteAsync(short code)
        {
            var res = await _service.DeleteAsync(code);
            return new JsonResult(res);
        }



    }
}

