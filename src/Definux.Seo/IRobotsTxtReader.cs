namespace Definux.Seo
{
    /// <summary>
    /// Service that read the robots.txt file from the main directory of the application.
    /// </summary>
    public interface IRobotsTxtReader
    {
        /// <summary>
        /// Gets the robots.txt file and converts it into string.
        /// </summary>
        /// <returns></returns>
        string GetRobotsTxt();
    }
}
