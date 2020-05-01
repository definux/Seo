using Definux.Seo.Results;
using System.Threading.Tasks;

namespace Definux.Seo
{
    public interface ISitemapBuilder
    {
        Task<SitemapResult> BuildSitemapAsync();
    }
}
