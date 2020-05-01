using Definux.Seo.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Definux.Seo.Models
{
    public abstract class PageSitemapPattern
    {
        private string baseUrl;

        public bool SinglePage { get; set; } = true;

        public string Domain { get; set; }

        public List<string> Patterns { get; set; } = new List<string>();

        public float Priority { get; set; } = 0.5f;

        public SeoChangeFrequencyTypes ChangeFrequency { get; set; } = SeoChangeFrequencyTypes.Always;

        public void SetBaseUrl(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        protected virtual async Task<List<string[]>> PrepareDataAsync()
        {
            return new List<string[]>();
        }

        public async Task<List<SitemapUrl>> BuildPatternUrlsAsync()
        {
            var result = new List<SitemapUrl>();
            string domain = string.IsNullOrWhiteSpace(Domain) ? this.baseUrl : Domain;
            var data = await PrepareDataAsync();

            foreach (var pattern in Patterns)
            {
                if (!SinglePage)
                {
                    foreach (var dataItemArgs in data)
                    {
                        string route = string.Format(pattern, dataItemArgs);
                        result.Add(new SitemapUrl
                        {
                            Location = $"{domain}{route}",
                            Priority = Priority.ToString(),
                            ChangeFrequency = ChangeFrequency.ToString()
                        });
                    }
                }
                else
                {
                    result.Add(new SitemapUrl
                    {
                        Location = $"{domain}{pattern}",
                        Priority = Priority.ToString(),
                        ChangeFrequency = ChangeFrequency.ToString()
                    });
                }
            }

            return result;
        }
    }
}
