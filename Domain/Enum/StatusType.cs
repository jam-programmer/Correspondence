using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum;

public enum StatusType
{
    [Display(Name = "ملاحظه نشده")]
    NotViewed, 
    [Display(Name = "در حال بررسی")]
    Reviewing,
    [Display(Name = "بایگانی")]
    Archive
}
