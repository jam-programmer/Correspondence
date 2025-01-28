using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class ReferralEntity:BaseEntity
{
    public string? UserName { set; get; }
    public string? FullName{ set;get;}
    public string? Description {  get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    #region Relation
    public LetterEntity? Letter { get; set; }
    public Guid LetterId { get; set; }
    #endregion
}
