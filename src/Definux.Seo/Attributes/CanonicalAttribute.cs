using Definux.Seo.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Definux.Seo.Attributes
{
    /// <summary>
    /// Attribute that define the canonical URL of the page.
    /// </summary>
    public class CanonicalAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanonicalAttribute"/> class.
        /// </summary>
        /// <param name="value"></param>
        public CanonicalAttribute(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Canonical URL value.
        /// </summary>
        public string Value { get; set; }

        /// <inheritdoc cref="ViewDataDictionary"/>
        public ViewDataDictionary ViewData { get; protected set; }

        /// <inheritdoc/>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = (Controller)context.Controller;
            this.ViewData = controller.ViewData;
            var metaTagsModel = this.ViewData.GetOrCreateCurrentMetaTagsModel();
            string value = this.Value;

            foreach (var routeParam in controller.RouteData.Values)
            {
                value = value.Replace($"{{{routeParam.Key}}}", routeParam.Value?.ToString() ?? string.Empty);
            }

            metaTagsModel.Canonical = value;
            this.ViewData.ApplyMetaTagsModel(metaTagsModel);

            base.OnActionExecuted(context);
        }

        /// <inheritdoc/>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var controller = (Controller)context.Controller;
            this.ViewData = controller.ViewData;
            this.ViewData.GetOrCreateCurrentMetaTagsModel();
        }
    }
}
