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
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddDefinuxSeo(this IServiceCollection services, Action<DefinuxSeoOptions> optionsAction = null)
        {
            var options = new DefinuxSeoOptions();
            optionsAction?.Invoke(options);

            services.AddScoped(typeof(ISitemapComposition), options.SitemapCompositionType);
            services.AddScoped<ISitemapBuilder, SitemapBuilder>();
            services.AddScoped<IRobotsTxtReader, RobotsTxtReader>();

            services.Configure<DefinuxSeoOptions>(definuxOptions =>
            {
                definuxOptions.DefaultMetaTags = options.DefaultMetaTags;
            });

            return services;
        }
    }
}
