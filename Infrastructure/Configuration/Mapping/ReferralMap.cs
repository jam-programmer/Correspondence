using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration.Mapping;

internal class ReferralMap :
    IEntityTypeConfiguration<ReferralEntity>
{
    public void Configure(EntityTypeBuilder<ReferralEntity> builder)
    {
        builder.ToTable("Referral"); builder.Property(p => p.Status).IsRequired();

        builder.Property(p => p.Description).IsRequired(false)
            .HasMaxLength(2500);
        builder.HasOne(o => o.Letter)
            .WithMany(m => m.Referrals)
            .HasForeignKey(f => f.LetterId);
    }
}
