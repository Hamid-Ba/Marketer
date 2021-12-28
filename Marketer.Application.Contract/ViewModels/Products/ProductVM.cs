using Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class ProductVM
    {
        public long Id { get; set; }
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Code { get;  set; }
        public string Title { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public int EachBoxCount { get; set; }
        public double Weight { get; set; }
        public double ConsumerPrice { get; set; }
        public double PurchasePrice { get; set; }
        public double Profit { get; set; }
        public int Count { get;  set; }
        public bool IsStock { get;  set; }
        public string ExpiredDate { get;  set; }
        public long OrderCount { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
    }

    public class CreateProductVM
    {
        [Display(Name = "برند")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long BrandId { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public IFormFile Picture { get; set; }

        [Display(Name = "جایگزین تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Display(Name = "تعداد در هر بسته")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int EachBoxCount { get; set; }

        [Display(Name = "وزن")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Weight { get; set; }

        [Display(Name = "قیمت مصرف کننده")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double ConsumerPrice { get; set; }

        [Display(Name = "قیمت خریدار")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double PurchacePrice { get; set; }

        [Display(Name = "تعداد در انبار")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Count { get; set; }

        [Display(Name = "تاریخ انقضاء")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ExpiredDate { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(1000, ErrorMessage = "حداکثر کاراکتر {1} می باشد")]
        public string MetaDescription { get; set; }
    }

    public class EditProductVM : CreateProductVM
    {
        public long Id { get; set; }
        public string PictureName { get; set; }
    }
}