using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Definux.Seo;
using Definux.Seo.Models;
using SampleSeo.Services;

namespace SampleSeo.Sitemap
{
    public class SitemapComposition : ISitemapComposition
    {
        private readonly ISampleDataStore sampleDataStore;

        public SitemapComposition(ISampleDataStore sampleDataStore)
        {
            this.sampleDataStore = sampleDataStore;
        }
        
        public async Task<IEnumerable<PageSitemapPattern>> SetupAsync()
        {
            var patterns = new List<PageSitemapPattern>();
            
            patterns.Add(new PageSitemapPattern
            {
                SinglePage = true,
                Patterns = new List<string> { "/home" },
                ChangeFrequency = SeoChangeFrequencyTypes.Always,
                Priority = 1f
            });
            
            patterns.Add(new PageSitemapPattern
            {
                SinglePage = true,
                Patterns = new List<string> { "/about" },
                ChangeFrequency = SeoChangeFrequencyTypes.Weekly,
                Priority = 0.5f
            });
            
            patterns.Add(new PageSitemapPattern
            {
                SinglePage = false,
                Patterns = new List<string> { "/fruits/{0}/{1}" },
                ChangeFrequency = SeoChangeFrequencyTypes.Daily,
                Priority = 0.7f,
                DataAccessor = async () =>
                {
                    var fruits = await this.sampleDataStore.GetFruitsAsync();
                    return fruits.Select(x => new [] { x.Id.ToString(), x.Name });
                }
            });

            return patterns;
        }
    }
}