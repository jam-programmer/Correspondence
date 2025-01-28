using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Mapping;

internal class ActionMap : IEntityTypeConfiguration<ActionEntity>
{
    public void Configure(EntityTypeBuilder<ActionEntity> builder)
    {
        builder.ToTable("Action");
        builder.Property(p => p.ActionTaker).IsRequired();
        builder.Property(p => p.Description).IsRequired()
            .HasMaxLength(2500);
        builder.HasOne(o => o.Letter)
            .WithMany(m => m.Actions)
            .HasForeignKey(f => f.LetterId);
    }
}
