using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace VitalScope.Api.Conventions;

public class LowerCaseControllerRouteConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            if (selector.AttributeRouteModel != null)
            {
                selector.AttributeRouteModel.Template = 
                    selector.AttributeRouteModel.Template?.Replace("[controller]",
                        controller.ControllerName.ToLowerInvariant());
            }
        }
    }
}