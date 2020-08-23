using Definux.Seo.Models;
using System;
using System.Collections.Generic;

namespace Definux.Seo.Options
{
    public class DefinuxSeoOptions
    {
        public DefinuxSeoOptions()
        {
            SitemapPatternsTypes = new List<Type>();
            DefaultMetaTags = new MetaTagsModel();
        }
        public IEnumerable<Type> SitemapPatternsTypes { get; set; }

        public MetaTagsModel DefaultMetaTags { get; set; }
    }
}
