using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Mapping;

internal class UserMap : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User");
        builder.Property(p => p.FullName).IsRequired().HasMaxLength(400);
        builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(200);
  

    }
}
