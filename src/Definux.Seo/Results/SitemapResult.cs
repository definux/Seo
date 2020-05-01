using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Definux.Seo.Results
{
    [XmlType(TypeName = "urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    [XmlRoot(Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9", IsNullable = false)]
    [Serializable]
    public class SitemapResult
    {
        public SitemapResult()
        {
            Urls = new List<SitemapUrl>();
        }

        [XmlElement("url")]
        public List<SitemapUrl> Urls { get; set; }

        public string ToSerializedSitemapXml()
        {
            using (var stringWriter = new System.IO.StringWriter())
            {
                var serializer = new XmlSerializer(GetType());
                serializer.Serialize(stringWriter, this);
                return stringWriter.ToString();
            }
        }
    }
}
