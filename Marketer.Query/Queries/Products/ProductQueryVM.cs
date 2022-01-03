using System;

namespace Marketer.Query.Queries.Products
{
    public class ProductQueryVM
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public int EachBoxCount { get; set; }
        public double Weight { get; set; }
        public string Description { get; set; }
        public double ConsumerPrice { get; set; }
        public double PurchasePrice { get; set; }
        public double Profit { get; set; }
        public int Count { get; set; }
        public bool IsStock { get; set; }
        public string ExpiredDate { get; set; }
        public long OrderCount { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public int? DiscountRate { get; set; }
        public DateTime CreationDate { get; set; }
    }
}