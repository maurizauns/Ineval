namespace Ineval.Models.Filters
{
    public class FieldFilter
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public FilterType Type { get; set; }
        public string UrlData { get; set; }
        public string DefaultValue { get; set; }
        public string Placeholder { get; set; }
        public bool IsDefaultValue { get; set; }
        public string Url { get; set; }
        public string Modelo { get; set; }
    }
}