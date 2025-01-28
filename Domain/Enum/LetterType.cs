using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum;

public enum LetterType
{
    [Display(Name = "ثبت شده")]
    Normal,
    [Display(Name = "پیش نویس")]
    Immediate
}
