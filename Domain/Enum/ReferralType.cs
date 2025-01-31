using System.ComponentModel.DataAnnotations;

namespace Domain.Enum;

public enum ReferralType
{
    [Display(Name = "گیرنده رونوشت")]
    Copy,
    [Display(Name = "گیرنده اصلی")]
    Original

}
