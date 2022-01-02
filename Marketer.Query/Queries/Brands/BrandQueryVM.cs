namespace Marketer.Query.Queries.Brands
{
    public class BrandQueryVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public long ProductCount { get; set; }
    }
}
