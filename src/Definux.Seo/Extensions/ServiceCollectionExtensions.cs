using System;
using System.Linq;
using System.Reflection;
using Definux.Seo.Models;
using Definux.Seo.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Definux.Seo.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register Definux SEO plugin.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="patternsAssembly"></param>
        /// <param name="optionsAction"></param>
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
