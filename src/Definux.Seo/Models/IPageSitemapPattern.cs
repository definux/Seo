using System.Collections.Generic;
using System.Threading.Tasks;
using Definux.Seo.Results;

namespace Definux.Seo.Models
{
    /// <summary>
    /// Definition of sitemap page structure. Its purpose is to defines the pattern of single/multiple page/s of the sitemap.
    /// </summary>
    public interface IPageSitemapPattern
    {
        /// <summary>
        /// Set the base URL of the page.
        /// </summary>
        /// <param name="baseUrl"></param>
        void SetBaseUrl(string baseUrl);

        /// <summary>
        /// Builds the collection of sitemap URLs that must be added to the <see cref="SitemapResult"/>.
        /// </summary>
        /// <returns></returns>
        Task<List<SitemapUrl>> BuildPatternUrlsAsync();
    }
}
