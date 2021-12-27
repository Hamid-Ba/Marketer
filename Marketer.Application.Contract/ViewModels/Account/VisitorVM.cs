using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Account
{
    public class VisitorVM
    {
        public long Id { get; set; }
        public string UniqueCode { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
        public long PlacedOrderCount { get; set; }
    }

    public class CreateVisitorVM
    {
        [Display(Name = "کد بازاریاب")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string UniqueCode { get; set; }

        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FullName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Password { get; set; }
    }

    public class EditVisitorVM
    {
        public long Id { get; set; }

        [Display(Name = "کد بازاریاب")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string UniqueCode { get; set; }

        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FullName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
    }


}
