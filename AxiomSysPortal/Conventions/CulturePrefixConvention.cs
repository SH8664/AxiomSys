using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace AxiomSysPortal.Conventions
{
    public class CulturePrefixConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            foreach (var selector in model.Selectors.ToList())
            {
                selector.AttributeRouteModel!.Template =
                    AttributeRouteModel.CombineTemplates("{culture=en}",
                        selector.AttributeRouteModel.Template);
            }
        }
    }
}
