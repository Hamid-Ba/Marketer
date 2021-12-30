using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Extera
{
    public class ContactUsVM
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string MobilePhone { get; set; }
        public string Message { get; set; }
        public string CreationDate { get; set; }
    }

    public class CreateContactUs
    {
        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(125, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string FullName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string MobilePhone { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(500, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Message { get; set; }
    }
}
