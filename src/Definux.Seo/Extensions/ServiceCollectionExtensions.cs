using Definux.Seo.Models;
using Definux.Seo.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Definux.Seo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register sitemap builder.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefinuxSeo(this IServiceCollection services, Assembly patternsAssembly, Action<DefinuxSeoOptions> optionsAction = null)
        {
            DefinuxSeoOptions options = new DefinuxSeoOptions();
            if (optionsAction != null)
            {
                optionsAction.Invoke(options);
            }

            services.AddScoped<ISitemapBuilder, SitemapBuilder>();
            services.AddScoped<IRobotsTxtReader, RobotsTxtReader>();

            var patternsInterfaces = patternsAssembly
                .GetTypes()
                .Where(x => x.GetInterfaces()
                    .Contains(typeof(IPageSitemapPattern)) && x.IsInterface);

            foreach (var patternInterface in patternsInterfaces)
            {
                var patternImplementation = patternsAssembly
                    .GetTypes()
                    .Where(x => x.GetInterfaces().Contains(patternInterface) && x.IsClass)
                    .FirstOrDefault();

                services.AddScoped(patternInterface, patternImplementation);
            }

            services.Configure<DefinuxSeoOptions>(definuxOptions =>
            {
                definuxOptions.SitemapPatternsTypes = patternsInterfaces;
                definuxOptions.DefaultMetaTags = options.DefaultMetaTags;
            });

            return services;
        }
    }
}
