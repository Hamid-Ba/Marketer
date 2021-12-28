using Framework.Application;
using System;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Discounts
{
    public class DiscountVM
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int DiscountRate { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
    }

    public class CreateDiscountVM
    {
        [Display(Name = "محصول")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ProductId { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, 100, ErrorMessage = ValidationMessage.IsRequired)]
        public int DiscountRate { get; set; }

        [Display(Name = "تاریخ شروع")]
        [MinLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [MaxLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [MinLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [MaxLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string EndDate { get; set; }

        [Display(Name = "دلیل")]
        public string Reason { get; set; }
    }

    public class EditDiscountVM : CreateDiscountVM
    {
        public long Id { get; set; }
    }
}