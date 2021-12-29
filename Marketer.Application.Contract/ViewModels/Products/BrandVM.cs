using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class BrandVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
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
    }

    public class SelectBrandVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
