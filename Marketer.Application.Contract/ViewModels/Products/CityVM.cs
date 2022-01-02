using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class CityVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateCityVM
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }
    }

    public class EditCityVM : CreateCityVM
    {
        public long Id { get; set; }
    }
}
