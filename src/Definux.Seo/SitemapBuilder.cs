using Definux.Seo.Models;
using Definux.Seo.Options;
using Definux.Seo.Results;
using Definux.Utilities.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Definux.Seo
{
    public sealed class SitemapBuilder : ISitemapBuilder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpContextAccessor httpContextAccessor;
        private List<IPageSitemapPattern> sitemapPatterns;
        private DefinuxSeoOptions options;
        private readonly string baseUrl;

        public SitemapBuilder(
            IHttpContextAccessor httpContextAccessor, 
            IServiceProvider serviceProvider,
            IOptions<DefinuxSeoOptions> optionsAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.serviceProvider = serviceProvider;
            this.options = optionsAccessor.Value;
            this.sitemapPatterns = new List<IPageSitemapPattern>();
            this.baseUrl = this.httpContextAccessor.HttpContext.GetAbsoluteRoute(string.Empty);
            if (this.options.SitemapPatternsTypes != null)
            {
                foreach (var type in this.options.SitemapPatternsTypes)
                {
                    AddSitemapPattern(type);
                }
            }
        }

        private void AddSitemapPattern(Type type)
        {
            var pageSitemapPattern = (IPageSitemapPattern)this.serviceProvider.GetService(type);
            pageSitemapPattern.SetBaseUrl(this.baseUrl);
            this.sitemapPatterns.Add(pageSitemapPattern);
        }

        public async Task<SitemapResult> BuildSitemapAsync()
        {
            var result = new SitemapResult();
            foreach (var pattern in this.sitemapPatterns)
            {
                result.Urls.AddRange(await pattern.BuildPatternUrlsAsync());
            }

            return result;
        }
    }
}
