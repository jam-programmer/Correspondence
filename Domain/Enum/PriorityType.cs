using System.ComponentModel.DataAnnotations;

namespace Domain.Enum;

public enum PriorityType
{
    [Display(Name = "عادی")]
    Normal,
    [Display(Name = "فوری")]
    Immediate,
    [Display(Name = "آنی")]
    Now
}
