using Definux.HtmlBuilder;
using Definux.Seo.Extensions;
using Definux.Seo.Models;
using Definux.Seo.Options;
using Definux.Utilities.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading.Tasks;

namespace Definux.Seo.TagHelpers
{
    [HtmlTargetElement("seo-meta-tags", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SeoMetaTagsTagHelper : TagHelper
    {
        private readonly MetaTagsModel defaultMetaTagsModel;

        public SeoMetaTagsTagHelper(IOptions<DefinuxSeoOptions> optionsAccessor)
        {
            defaultMetaTagsModel = optionsAccessor.Value.DefaultMetaTags;
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            ProcessTagHelper(context, output);

            base.Process(context, output);
        }

        protected virtual void ProcessTagHelper(TagHelperContext context, TagHelperOutput output)
        {
            var metaTagsModel = ViewContext.ViewData.GetMetaTagsModelOrDefault();
            if (metaTagsModel != null)
            {
                metaTagsModel.ApplyStaticTags(this.defaultMetaTagsModel);

                output.TagName = HtmlTags.Link.Name;
                output.TagMode = TagMode.StartTagOnly;
                output.Attributes.Add("rel", "canonical");
                output.Attributes.Add("href", ViewContext.HttpContext.GetAbsoluteRoute(metaTagsModel.Canonical));

                StringBuilder tagsBuilder = new StringBuilder();
                tagsBuilder.AppendLine($"<meta charset=\"{metaTagsModel.Charset}\" />");

                var startMetaTags = metaTagsModel.GetStartMetaTags();
                foreach (var startMetaTag in startMetaTags)
                {
                    tagsBuilder.AppendLine($"<meta {startMetaTag.KeyName}=\"{startMetaTag.Key}\" {startMetaTag.ValueName}=\"{startMetaTag.Value}\" />");
                }

                tagsBuilder.AppendLine($"<title>{metaTagsModel.Title.Value}</title>");

                var filledMetaTags = metaTagsModel.GetFilledMetaTags();
                foreach (var filledMetaTag in filledMetaTags)
                {
                    tagsBuilder.AppendLine($"<meta {filledMetaTag.KeyName}=\"{filledMetaTag.Key}\" {filledMetaTag.ValueName}=\"{filledMetaTag.Value}\" />");
                }

                output.PreElement.SetHtmlContent(new HtmlString(tagsBuilder.ToString()));
            }
        }
    }
}
