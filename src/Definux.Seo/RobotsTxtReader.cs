using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Definux.Seo
{
    public sealed class RobotsTxtReader : IRobotsTxtReader
    {
        private const string RobotsTxtFileName = "robots.txt";
        private readonly string contentRootPath;

        public RobotsTxtReader(IHostingEnvironment hostingEnvironment)
        {
            this.contentRootPath = hostingEnvironment.ContentRootPath;
        }
        public string GetRobotsTxt()
        {
            try
            {
                string filePath = Path.Combine(this.contentRootPath, RobotsTxtFileName);
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("The robots.txt was not found in the content root path of the application.");
                }

                string fileContent = File.ReadAllText(filePath);

                return fileContent;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
