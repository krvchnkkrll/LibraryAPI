using LibraryAPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.ModelConfiguration;

public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
{
    public void Configure(EntityTypeBuilder<UserBook> builder)
    {
        builder
            .ToTable("UserBooks")
            .HasKey(ub => ub.Id);
        
        builder
            .Property(ub => ub.DateReceipt)
            .IsRequired();

        builder
            .Property(ub => ub.BorrowPeriod)
            .IsRequired()
            .HasDefaultValue(14);
        
        builder
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBooks)
            .HasForeignKey(ub => ub.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(ub => ub.Book)
            .WithMany(b => b.UserBooks)
            .HasForeignKey(ub => ub.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}