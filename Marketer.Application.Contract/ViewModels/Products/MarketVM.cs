using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace Marketer.Application.Contract.ViewModels.Products
{
    public class MarketVM
    {
        public long Id { get; set; }
        public long VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string MobilePhone { get; set; }
    }

    public class CreateMarketVM
    {
        public long VisitorId { get; set; }

        [Display(Name = "نام مارکت")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "صاحب مارکت")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Owner { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4|5|6|7|8|9|0]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره را به فرم صحیح وارد نمایید")]
        public string MobilePhone { get; set; }
    }

    public class EditMarketVM : CreateMarketVM
    {
        public long Id { get; set; }
    }
}