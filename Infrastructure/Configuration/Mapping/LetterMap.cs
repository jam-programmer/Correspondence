using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Mapping;

internal class LetterMap : IEntityTypeConfiguration<LetterEntity>
{
    public void Configure(EntityTypeBuilder<LetterEntity> builder)
    {
        builder.ToTable("Letter");
       
        builder.Property(p => p.Referrer).IsRequired();
        builder.Property(p => p.Type).IsRequired();
        builder.Property(p => p.Priority).IsRequired();
        builder.Property(p => p.Status).IsRequired();
        builder.Property(p => p.Text).IsRequired();
        builder.Property(p => p.Title).IsRequired()
            .HasMaxLength(900);
    }
}
