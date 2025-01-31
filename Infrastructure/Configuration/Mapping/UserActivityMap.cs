using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mapping;

internal class UserActivityMap : IEntityTypeConfiguration<UserActivityEntity>
{
    public void Configure(EntityTypeBuilder<UserActivityEntity> builder)
    {
        builder.ToTable("UserActivity");
        builder.HasOne(o => o.User)
            .WithMany(m => m.UserActivities)
            .HasForeignKey(o => o.UserId);
    }
}
