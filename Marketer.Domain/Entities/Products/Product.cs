using Framework.Domain;
using System;

namespace Marketer.Domain.Entities.Products
{
    public class Product : EntityBase
    {
        public long BrandId { get; private set; }
        public long CategoryId { get; private set; }
        public long PackageTypeId { get; set; }
        public string Code { get; private set; }
        public string Title { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public int Count { get; private set; }
        //public int EachBoxCount { get; private set; }
        public double ConsumerPrice { get; private set; }
        public double PurchacePrice { get; private set; }
        public double Profit { get; private set; }
        public string PackageValue { get; private set; }
        //public double Weight { get; private set; }
        public string Description { get; private set; }
        public bool IsStock { get; private set; }
        public DateTime ExpiredDate { get; private set; }
        public long OrderCount { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public bool IsAmazedProduct { get; private set; }

        public Category Category { get; private set; }
        public Brand Brand { get; private set; }
        public PackageType PackageType { get; set; }

        public Product(long brandId, long categoryId,long packageTypeId, string code, string title, string picture, string pictureAlt, string pictureTitle, int count,
          double consumerPrice, double purchacePrice, string packageValue,string description, DateTime expiredDate,
          string slug, string keywords, string metaDescription)
        {
            BrandId = brandId;
            CategoryId = categoryId;
            PackageTypeId = packageTypeId;
            Code = code;
            Title = title;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Count = count;
            //EachBoxCount = eachBoxCount;
            ConsumerPrice = consumerPrice;
            PurchacePrice = purchacePrice;
            Profit = ConsumerPrice - PurchacePrice;
            //Weight = weight;
            PackageValue = packageValue;
            Description = description;
            IsStock = count > 0;
            ExpiredDate = expiredDate;
            OrderCount = 0;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            IsAmazedProduct = false;
        }

        public void Edit(long brandId, long categoryId,long packageTypeId, string code, string title, string picture, string pictureAlt, string pictureTitle, int count,
            double consumerPrice, double purchacePrice, string packageValue,string description, DateTime expiredDate,
            string slug, string keywords, string metaDescription)
        {
            BrandId = brandId;
            CategoryId = categoryId;
            PackageTypeId = packageTypeId;
            Code = code;
            Title = title;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Count = count;
            //EachBoxCount = eachBoxCount;
            ConsumerPrice = consumerPrice;
            PurchacePrice = purchacePrice;
            Profit = ConsumerPrice - PurchacePrice;
            //Weight = weight;
            PackageValue = packageValue;
            Description = description;
            IsStock = count > 0;
            ExpiredDate = expiredDate;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public void AddOrder(int reduceStock)
        {
            ++OrderCount;
            Count -= reduceStock;
        }

        public void AmazedProduct(bool isAmazed) => IsAmazedProduct = isAmazed;

        public bool IsInStock()
        {
            if (Count <= 0) return false;
            return true;
        }

        public int AddCount(int count) => Count += count;

        public void ReduceCount(int count) => Count -= count;
            
    }
}