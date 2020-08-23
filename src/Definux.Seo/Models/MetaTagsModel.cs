using System.Collections.Generic;

namespace Definux.Seo.Models
{
    public class MetaTagsModel
    {
        public string TitleSuffix { get; set; }

        public string Canonical { get; set; }

        public string Charset { get; set; } = "utf-8";

        public MetaTag Viewport { get; } = new MetaTag("viewport", "width=device-width, initial-scale=1.0");
        public MetaTag Robots { get; } = new MetaTag("robots", "index, follow");
        public MetaTag Title { get; } = new MetaTag("title");
        public MetaTag Description { get; } = new MetaTag("description");
        public MetaTag Keywords { get; } = new MetaTag("keywords");
        public MetaTag Author { get; } = new MetaTag("author");
        public MetaTag OpenGraphType { get; } = new MetaTag("og:type") { KeyName = "property" };
        public MetaTag OpenGraphTitle { get; } = new MetaTag("og:title") { KeyName = "property" };
        public MetaTag OpenGraphDescription { get; } = new MetaTag("og:description") { KeyName = "property" };
        public MetaTag OpenGraphImage { get; } = new MetaTag("og:image") { KeyName = "property" };
        public MetaTag OpenGraphUrl { get; } = new MetaTag("og:url") { KeyName = "property" };
        public MetaTag OpenGraphSiteName { get; } = new MetaTag("og:site_name") { KeyName = "property" };
        public MetaTag FacebookAppId { get; } = new MetaTag("fb:app_id") { KeyName = "property" };
        public MetaTag TwitterCard { get; } = new MetaTag("twitter:card");
        public MetaTag TwitterTitle { get; } = new MetaTag("twitter:title");
        public MetaTag TwitterDescription { get; } = new MetaTag("twitter:description");
        public MetaTag TwitterImage { get; } = new MetaTag("twitter:image");
        public MetaTag TwitterSite { get; } = new MetaTag("twitter:site");
        public MetaTag TwitterCreator { get; } = new MetaTag("twitter:creator");

        public void SetTitle(string title)
        {
            Title.Value = title + TitleSuffix;
            OpenGraphTitle.Value = title + TitleSuffix;
            TwitterTitle.Value = title + TitleSuffix;
        }

        public void SetDescription(string description)
        {
            Description.Value = description;
            OpenGraphDescription.Value = description;
            TwitterDescription.Value = description;
        }

        public void SetImage(string imageUrl)
        {
            OpenGraphImage.Value = imageUrl;
            TwitterImage.Value = imageUrl;
        }

        public void SetMetaTag(MainMetaTags metaTag, string value)
        {
            switch (metaTag)
            {
                case MainMetaTags.Title:
                    SetTitle(value);
                    break;
                case MainMetaTags.Description:
                    SetDescription(value);
                    break;
                case MainMetaTags.Keywords:
                    Keywords.Value = value;
                    break;
                case MainMetaTags.Image:
                    SetImage(value);
                    break;
                case MainMetaTags.Author:
                    Author.Value = value;
                    break;
                default:
                    break;
            }
        }


        public List<MetaTag> GetStartMetaTags()
        {
            var result = new List<MetaTag>();

            if (Viewport.HasValue) { result.Add(Viewport); }
            if (Robots.HasValue) { result.Add(Robots); }

            return result;
        }

        public List<MetaTag> GetFilledMetaTags()
        {
            var result = new List<MetaTag>();

            if (Title.HasValue) { result.Add(Title); }
            if (Description.HasValue) { result.Add(Description); }
            if (Keywords.HasValue) { result.Add(Keywords); }
            if (Author.HasValue) { result.Add(Author); }
            if (OpenGraphType.HasValue) { result.Add(OpenGraphType); }
            if (OpenGraphTitle.HasValue) { result.Add(OpenGraphTitle); }
            if (OpenGraphDescription.HasValue) { result.Add(OpenGraphDescription); }
            if (OpenGraphImage.HasValue) { result.Add(OpenGraphImage); }
            if (OpenGraphUrl.HasValue) { result.Add(OpenGraphUrl); }
            if (OpenGraphSiteName.HasValue) { result.Add(OpenGraphSiteName); }
            if (FacebookAppId.HasValue) { result.Add(FacebookAppId); }
            if (TwitterCard.HasValue) { result.Add(TwitterCard); }
            if (TwitterTitle.HasValue) { result.Add(TwitterTitle); }
            if (TwitterDescription.HasValue) { result.Add(TwitterDescription); }
            if (TwitterImage.HasValue) { result.Add(TwitterImage); }
            if (TwitterSite.HasValue) { result.Add(TwitterSite); }
            if (TwitterCreator.HasValue) { result.Add(TwitterCreator); }

            return result;
        }

        public void ApplyStaticTags(MetaTagsModel model)
        {
            TitleSuffix = model.TitleSuffix;
            Charset = model.Charset;

            Viewport.Value = model.Viewport.Value;
            Robots.Value = model.Robots.Value;
            OpenGraphSiteName.Value = model.OpenGraphSiteName.Value;
            OpenGraphType.Value = model.OpenGraphType.Value;
            FacebookAppId.Value = model.FacebookAppId.Value;
            TwitterCard.Value = model.TwitterCard.Value;
            TwitterCreator.Value = model.TwitterCreator.Value;
            TwitterSite.Value = model.TwitterSite.Value;

            SetTitle(Title.Value);
            SetDescription(Description.Value);
            SetImage(OpenGraphImage.Value);
        }
    }
}
