using Definux.Seo.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Definux.Seo
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class DefinuxSeoController : Controller
    {
        [HttpGet]
        [Produces("text/plain")]
        [Route("/robots.txt")]
        public IActionResult Robots([FromServices]IRobotsTxtReader robotsTxtReader)
        {
            return Ok(robotsTxtReader.GetRobotsTxt());
        }

        [HttpGet]
        [Produces("application/xml")]
        [Route("/sitemap.xml")]
        public async Task<IActionResult> Sitemap([FromServices]ISitemapBuilder sitemapBuilder)
        {
            SitemapResult sitemapResult = await sitemapBuilder.BuildSitemapAsync();

            return Ok(sitemapResult);
        }
    }
}
