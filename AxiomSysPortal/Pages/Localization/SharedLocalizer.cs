using System.Globalization;

namespace AxiomSysPortal.Pages.Localization
{
    public class SharedLocalizer : ISharedLocalizer
    {
        private static readonly Dictionary<string, (string En, string Ar)> _t = new()
        {
            ["32"] = ("Add", "إضافة"),
            ["5583"] = ("Edit", "تعديل"),
            ["18"] = ("Delete", "حذف"),
            ["3"] = ("Code", "الرمز"),
            ["4"] = ("English name", "الاسم الإنجليزي"),
            ["5"] = ("Arabic name", "الاسم العربي"),
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
