using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Mapping;

internal class AttachmentMap : IEntityTypeConfiguration<AttachmentEntity>
{
    public void Configure(EntityTypeBuilder<AttachmentEntity> builder)
    {
        builder.ToTable("Attachment");
        builder.HasOne(o => o.Letter)
             .WithMany(m => m.Attachments)
             .HasForeignKey(f => f.LetterId);
    }
}
