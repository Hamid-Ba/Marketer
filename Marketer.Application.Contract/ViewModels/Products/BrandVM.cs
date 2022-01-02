using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class BrandVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
        public long ProductCount { get; set; }
    }

    public class CreateBrandVM
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public IFormFile Picture { get; set; }

        [Display(Name = "جایگزین تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string KeyWords { get; set; }

        [Display(Name = "توضیحات متا")]
        public string MetaDescription { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }
    }

    public class EditBrandVM : CreateBrandVM
    {
        public long Id { get; set; }
        public string PictureName { get; set; }
    }

    public class SelectBrandVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
