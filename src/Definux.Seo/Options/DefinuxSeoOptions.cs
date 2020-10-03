using System;
using System.Collections.Generic;
using Definux.Seo.Models;

namespace Definux.Seo.Options
{
    /// <summary>
    /// Implementation of Definux SEO plugin options.
    /// </summary>
    public class DefinuxSeoOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefinuxSeoOptions"/> class.
        /// </summary>
        public DefinuxSeoOptions()
        {
            this.SitemapPatternsTypes = new List<Type>();
            this.DefaultMetaTags = new MetaTagsModel();
        }

        /// <summary>
        /// Collection of all sitemap patterns types which will be used for sitemap generation.
        /// </summary>
        public IEnumerable<Type> SitemapPatternsTypes { get; set; }

        /// <summary>
        /// Default meta tags model.
        /// </summary>
        public MetaTagsModel DefaultMetaTags { get; set; }
    }
}
