using System.ComponentModel.DataAnnotations;

namespace Framework.Application
{
    public enum ProductSort
    {
        [Display(Name = "جدیدترین")]
        Newest,

        [Display(Name = "قدیمی ترین")]
        Oldest,

        [Display(Name = "ارزان ترین")]
        Cheapest,

        [Display(Name = "گران ترین")]
        Expencive
    }
}
