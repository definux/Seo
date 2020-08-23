namespace Definux.Seo.Models
{
    public class MetaTag
    {
        public MetaTag()
        {

        }

        public MetaTag(string key)
        {
            Key = key;
        }

        public MetaTag(string name, string value)
        {
            Key = name;
            Value = value;
        }

        public string KeyName { get; set; } = "name";

        public string ValueName { get; set; } = "content";

        public string Key { get; set; }

        public string Value { get; set; }

        public bool HasValue 
        { 
            get
            {
                return !string.IsNullOrEmpty(Value);
            }
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
