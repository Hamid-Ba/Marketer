using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class PackageTypeVM
	{
        public long Id { get; set; }
        public string Title { get; set; }
    }

    public class CreatePackageTypeVM
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }
    }

    public class EditPackageTypeVM : CreatePackageTypeVM
    {
        public long Id { get; set; }
    }

    public class DeletePackageTypeVM : EditPackageTypeVM { }
    
}