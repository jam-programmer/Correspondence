using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum;

public enum DocumentType
{
      [Display(Name = "نامه")] Letter
    , [Display(Name = "اطلاعیه")] Notice
    , [Display(Name = "بخشنامه")] Circular
    , [Display(Name = "قرارداد")] Contract
    , [Display(Name = "مصوبه")] Approval
    , [Display(Name = "مناقصه")] Tender
    , [Display(Name = "گزارش")] Report
    , [Display(Name = "ابلاغیه")] Notification
}
