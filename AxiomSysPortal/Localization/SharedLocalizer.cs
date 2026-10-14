using System.Globalization;

namespace AxiomSysPortal.Localization
{
    public class SharedLocalizer : ISharedLocalizer
    {
        private static readonly Dictionary<string, (string En, string Ar)> _t = new()
        {
            ["32"] = ("Add", "إضافة"),
            ["5583"] = ("Edit", "تعديل"),
            ["18"] = ("Delete", "حذف"),

            ["3"] = ("Code", "الكود"),
            ["4"] = ("English name", "الاسم الإنجليزي"),
            ["5"] = ("Arabic name", "الاسم العربي"),
            ["6"] = ("Days", "عدد الأيام"),
            ["7"] = ("Active", "نشط"),
            ["8"] = ("Yes", "نعم"),
            ["9"] = ("No", "لا"),
            ["10"] = ("Save", "حفظ"),
            ["11"] = ("Cancel", "إلغاء"),
            ["12"] = ("Delivery Terms Configuration", "إعدادات شروط التسليم"),
            ["13"] = ("Home", "الرئيسية"),
            ["14"] = ("Logout", "تسجيل الخروج"),
            ["15"] = ("Login", "دخول"),
            ["16"] = ("Username", "اسم المستخدم"),
            ["17"] = ("Password", "كلمة المرور"),
            ["19"] = ("Invalid username or password", "اسم المستخدم أو كلمة المرور غير صحيحة"),
            ["20"] = ("Please enter your username and password", "الرجاء إدخال اسم المستخدم وكلمة المرور"),
            ["21"] = ("Privacy", "الخصوصية"),
            ["22"] = ("Register", "تسجيل"),
            ["23"] = ("No Account ? ", "لا يوجد حساب ؟ "),
            ["24"] = ("Already have an account?", "لديك حساب بالفعل؟"),
            ["25"] = ("Create account", "إنشاء حساب"),
            ["26"] = ("Use this page to detail your site's privacy policy.", "استخدم هذه الصفحة لتفصيل سياسة الخصوصية لموقعك."),
            ["27"] = ("Welcome", "مرحبا"),
            ["28"] = ("Learn about", "تعرف على"),
            ["29"] = ("building Web apps with ASP.NET Core", "بناء تطبيقات الويب باستخدام ASP.NET Core"),
            ["30"] = ("Privacy Policy", "سياسة الخصوصية"),
            ["31"] = ("Sales Delivery Terms", "شروط تسليم المبيعات"),
            ["33"] = ("Email", "البريد الإلكتروني"),
            ["34"] = ("Registration failed", "فشل التسجيل"),
            ["35"] = ("Account created. Please log in.", "تم إنشاء الحساب. الرجاء تسجيل الدخول."),
            ["36"] = ("Invalid email or password", "البريد الإلكتروني أو كلمة المرور غير صحيحة"),
            ["37"] = ("Login failed", "فشل تسجيل الدخول"),
            ["38"] = ("Term added successfully!", "تمت إضافة الشرط بنجاح!")
        };

        public string this[string key]
        {
            get
            {
                if (!_t.TryGetValue(key, out var v)) return key;   // show the key if missing
                return CultureInfo.CurrentUICulture.Name.StartsWith("ar") ? v.Ar : v.En;
            }
        }
    }
}
