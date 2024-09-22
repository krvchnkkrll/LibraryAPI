using LibraryAPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.ModelConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("User")
            .HasKey(u => u.Id);
        
        builder
            .Property(u => u.Surname)
            .IsRequired()
            .HasMaxLength(30);
        
        builder
            .Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(30);
        
        builder
            .Property(u => u.Patronymic)
            .HasMaxLength(30);

        builder
            .Property(u => u.Login)
            .IsRequired()
            .HasMaxLength(30);

        builder
            .Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(30);

        builder
            .Property(u => u.IsItStaff)
            .IsRequired()
            .HasDefaultValue(false);

        builder
            .HasMany(u => u.UserBooks)
            .WithOne(ub => ub.User)
            .HasForeignKey(ub => ub.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}