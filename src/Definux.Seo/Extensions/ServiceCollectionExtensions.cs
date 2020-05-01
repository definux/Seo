using Definux.Seo.Models;
using Definux.Seo.Options;
using Microsoft.Extensions.DependencyInjection;
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
        public static IServiceCollection AddDefinuxSeo(this IServiceCollection services, Assembly patternsAssembly)
        {
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

                services.AddTransient(patternInterface, patternImplementation);
            }

            services.Configure<DefinuxSeoOptions>(options =>
            {
                options.SitemapPatternsTypes = patternsInterfaces;
            });

            return services;
        }
    }
}
