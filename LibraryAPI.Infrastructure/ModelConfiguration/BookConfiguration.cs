using LibraryAPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.ModelConfiguration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .ToTable("Books")
            .HasKey(b => b.Id);
        
        builder
            .Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder
            .Property(b => b.Genre)
            .IsRequired()
            .HasConversion<string>();
        
        builder
            .Property(b => b.BookStatus)
            .IsRequired()
            .HasConversion<string>();
        
        builder
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(b => b.UserBooks)
            .WithOne(ub => ub.Book)
            .HasForeignKey(ub => ub.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}