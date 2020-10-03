using System.Threading.Tasks;
using Definux.Seo.Results;

namespace Definux.Seo
{
    /// <summary>
    /// Service that process and build sitemap of the application.
    /// </summary>
    public interface ISitemapBuilder
    {
        /// <summary>
        /// Builds sitemap based on applied sitemap patterns.
        /// </summary>
        /// <returns></returns>
        Task<SitemapResult> BuildSitemapAsync();
    }
}
