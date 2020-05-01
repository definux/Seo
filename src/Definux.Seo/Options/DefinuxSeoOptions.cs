using System;
using System.Collections.Generic;

namespace Definux.Seo.Options
{
    public class DefinuxSeoOptions
    {
        public DefinuxSeoOptions()
        {
            SitemapPatternsTypes = new List<Type>();
        }
        public IEnumerable<Type> SitemapPatternsTypes { get; set; }
    }
}
