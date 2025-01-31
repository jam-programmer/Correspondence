using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class AttachmentEntity:BaseEntity
{
    public string? Path {  get; set; }
    #region Relation
    public LetterEntity? Letter { get; set; }
    public Guid LetterId { get; set; }
    #endregion
}
