using LibraryAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.DbContext;

public class LibraryInfoContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Author> Authors { get; set; } = null!;
    
    public DbSet<Book> Books { get; set; } = null!;
    
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<UserBook> UserBooks { get; set; } = null!;
    
    public LibraryInfoContext(DbContextOptions<LibraryInfoContext> options)
        : base(options)
    {
        
    }
}
