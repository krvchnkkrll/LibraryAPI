using LibraryAPI.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryAPI.Infrastructure.ModelConfiguration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .ToTable("Author")
            .HasKey(b => b.Id);

        builder
            .Property(a => a.Surname)
            .IsRequired()
            .HasMaxLength(30);
        
        builder
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(30);
        
        builder
            .Property(a => a.Patronymic)
            .HasMaxLength(30);

        builder
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}