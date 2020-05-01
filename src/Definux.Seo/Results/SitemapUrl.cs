using System.Xml.Serialization;

namespace Definux.Seo.Results
{
    public class SitemapUrl
    {
        [XmlElement("loc")]
        public string Location { get; set; }

        [XmlElement("lastmod")]
        public string LastModification { get; set; }

        [XmlElement("changefreq")]
        public string ChangeFrequency { get; set; }

        [XmlElement("priority")]
        public string Priority { get; set; }
    }
}
