using System.Collections.Generic;
using System.Threading.Tasks;
using Definux.Seo.Results;

namespace Definux.Seo.Models
{
    /// <inheritdoc cref="IPageSitemapPattern"/>
    public abstract class PageSitemapPattern
    {
        private string baseUrl;

        /// <summary>
        /// Flag indicates that the pattern will be applied into a single page or not.
        /// </summary>
        public bool SinglePage { get; set; } = true;

        /// <summary>
        /// Domain name of the current sitemap page. This property is with priority than base URL.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Collection of all page patterns which will be applied for the current object URL generation.
        /// </summary>
        public List<string> Patterns { get; set; } = new List<string>();

        /// <summary>
        /// Priority of the sitemap page for search engines.
        /// </summary>
        public float Priority { get; set; } = 0.5f;

        /// <inheritdoc cref="SeoChangeFrequencyTypes"/>
        public SeoChangeFrequencyTypes ChangeFrequency { get; set; } = SeoChangeFrequencyTypes.Always;

        /// <inheritdoc cref="IPageSitemapPattern.SetBaseUrl(string)"/>
        public void SetBaseUrl(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        /// <inheritdoc cref="IPageSitemapPattern.BuildPatternUrlsAsync()"/>
        public async Task<List<SitemapUrl>> BuildPatternUrlsAsync()
        {
            var result = new List<SitemapUrl>();
            string domain = string.IsNullOrWhiteSpace(this.Domain) ? this.baseUrl : this.Domain;
            var data = await this.PrepareDataAsync();

            foreach (var pattern in this.Patterns)
            {
                if (!this.SinglePage)
                {
                    foreach (var dataItemArgs in data)
                    {
                        string route = string.Format(pattern, dataItemArgs);
                        result.Add(new SitemapUrl
                        {
                            Location = $"{domain}{route}",
                            Priority = this.Priority.ToString(),
                            ChangeFrequency = this.ChangeFrequency.ToString(),
                        });
                    }
                }
                else
                {
                    result.Add(new SitemapUrl
                    {
                        Location = $"{domain}{pattern}",
                        Priority = this.Priority.ToString(),
                        ChangeFrequency = this.ChangeFrequency.ToString(),
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Method which main purpose is to be used for data preparing for the sitemap pattern item.
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<List<string[]>> PrepareDataAsync()
        {
            return new List<string[]>();
        }
    }
}
