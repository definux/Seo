using Definux.Seo.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Definux.Seo.Models
{
    public interface IPageSitemapPattern
    {
        void SetBaseUrl(string baseUrl);

        Task<List<SitemapUrl>> BuildPatternUrlsAsync();
    }
}
