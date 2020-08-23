using Definux.Seo.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Definux.Seo.Attributes
{
    public class CanonicalAttribute : ActionFilterAttribute
    {
        public CanonicalAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public ViewDataDictionary ViewData { get; protected set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = (Controller)context.Controller;
            ViewData = controller.ViewData;
            var metaTagsModel = ViewData.GetOrCreateCurrentMetaTagsModel();
            string value = Value;

            foreach (var routeParam in controller.RouteData.Values)
            {
                value = value.Replace($"{{{routeParam.Key}}}", routeParam.Value?.ToString() ?? string.Empty);
            }

            metaTagsModel.Canonical = value;
            ViewData.ApplyMetaTagsModel(metaTagsModel);

            base.OnActionExecuted(context);
        }
    }
}
