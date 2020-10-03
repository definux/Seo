using System.Threading.Tasks;
using Definux.Seo.Results;
using Microsoft.AspNetCore.Mvc;

namespace Definux.Seo
{
    /// <summary>
    /// Main controller of Definux SEO plugin.
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class DefinuxSeoController : Controller
    {
        /// <summary>
        /// Action of the robots.txt file.
        /// </summary>
        /// <param name="robotsTxtReader"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("text/plain")]
        [Route("/robots.txt")]
        public IActionResult Robots([FromServices]IRobotsTxtReader robotsTxtReader)
        {
            return this.Ok(robotsTxtReader.GetRobotsTxt());
        }

        /// <summary>
        /// Action of the sitemap.xml file.
        /// </summary>
        /// <param name="sitemapBuilder"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/xml")]
        [Route("/sitemap.xml")]
        public async Task<IActionResult> Sitemap([FromServices]ISitemapBuilder sitemapBuilder)
        {
            SitemapResult sitemapResult = await sitemapBuilder.BuildSitemapAsync();
            return this.Ok(sitemapResult);
        }
    }
}
