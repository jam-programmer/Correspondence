using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class LetterEntity : BaseEntity
{
    public string? Number {  get; set; }    
    public string? Title {  get; set; }
    public string? Text {  get; set; }
    public string? Referrer { get; set; }

    public LetterType Type {  get; set; } 
    public StatusType Status {  get; set; } 
    public PriorityType Priority { get; set; }
    public DateTime CreateAt { get; set; }=DateTime.Now;
    #region Relation
  
    public ICollection<ActionEntity>? Actions { get; set; }
    public ICollection<ReferralEntity>? Referrals { get; set; }

    #endregion
}
