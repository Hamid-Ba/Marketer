using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class BrandVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ProductCount { get; set; }
    }

    public class CreateBrandVM
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }
    }

    public class EditBrandVM : CreateBrandVM
    {
        public long Id { get; set; }
    }
}
