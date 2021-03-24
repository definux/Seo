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
            this.DefaultMetaTags = new MetaTagsModel();
        }

        /// <summary>
        /// Implementation type of <see cref="ISitemapComposition"/>.
        /// </summary>
        public Type SitemapCompositionType { get; private set; }

        /// <summary>
        /// Default meta tags model.
        /// </summary>
        public MetaTagsModel DefaultMetaTags { get; set; }

        /// <summary>
        /// Set type
        /// </summary>
        /// <typeparam name="TSitemapComposition">Sitemap composition implementation type.</typeparam>
        public void SetSitemapComposition<TSitemapComposition>()
            where TSitemapComposition : class, ISitemapComposition
        {
            this.SitemapCompositionType = typeof(TSitemapComposition);
        }
    }
}
