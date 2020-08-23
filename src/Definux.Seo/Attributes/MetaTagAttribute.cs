using Definux.Seo.Extensions;
using Definux.Seo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Definux.Seo.Attributes
{
    public class MetaTagAttribute : ActionFilterAttribute
    {
        public MetaTagAttribute(MainMetaTags type, string value, bool extractValueFromViewData = false)
        {
            Type = type;
            Value = value;
            ExtractValueFromViewData = extractValueFromViewData;
        }

        public MainMetaTags Type { get; set; }

        public bool ExtractValueFromViewData { get; set; }

        public string Value { get; set; }

        public ViewDataDictionary ViewData { get; protected set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = (Controller)context.Controller;
            ViewData = controller.ViewData;

            var metaTagsModel = ViewData.GetOrCreateCurrentMetaTagsModel();
            string value = Value;
            if (ExtractValueFromViewData)
            {
                value = ViewData.ContainsKey(Value) ? ViewData[Value]?.ToString() : string.Empty;
            }

            metaTagsModel.SetMetaTag(Type, value);
            ViewData.ApplyMetaTagsModel(metaTagsModel);

            base.OnActionExecuted(context);
        }
    }
}
