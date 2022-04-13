using BoilerPlate.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace BoilerPlate.Bootstrapper
{
    public class LocalizationConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            var culturePrefix = new AttributeRouteModel(new RouteAttribute("{culture}"));

            foreach (var controller in application.Controllers)
            {
                foreach (var action in controller.Actions)
                {
                    var attributes = action.Attributes.OfType<RouteAttribute>().ToArray();
                    foreach (var attribute in attributes)
                    {
                        SelectorModel defaultSelector = action.Selectors.First();

                        var oldAttributeRouteModel = defaultSelector.AttributeRouteModel;
                        var newAttributeRouteModel = new AttributeRouteModel(new RouteAttribute("{culture}"));

                        newAttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(culturePrefix, defaultSelector.AttributeRouteModel);

                        if (!action.Selectors.Any(s => s.AttributeRouteModel.Template == newAttributeRouteModel.Template))
                        {
                            action.Selectors.Insert(0, new SelectorModel(defaultSelector)
                            {
                                AttributeRouteModel = newAttributeRouteModel
                            });
                        }
                    }
                }
            }
        }
    }
}
