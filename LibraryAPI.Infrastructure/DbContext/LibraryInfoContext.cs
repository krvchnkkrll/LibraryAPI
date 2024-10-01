using System.Reflection;
using LibraryAPI.Core.Models.Entities;
using LibraryAPI.Infrastructure.LoggingEntities;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.DbContext;

public class LibraryInfoContext : Microsoft.EntityFrameworkCore.DbContext
{
    private readonly EntityChangeLogging _changeLogging;
    public DbSet<Author> Authors { get; set; } = null!;
    
    public DbSet<Book> Books { get; set; } = null!;
    
    public DbSet<User> Users { get; set; } = null!;
    
    public DbSet<UserBook> UserBooks { get; set; } = null!;
    
    public LibraryInfoContext(DbContextOptions<LibraryInfoContext> options, EntityChangeLogging changeLogging)
        : base(options)
    {
        _changeLogging = changeLogging ?? throw new ArgumentNullException(nameof(changeLogging));;
    }
    /*
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    */
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        LogChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void LogChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => 
                e.State == EntityState.Added ||
                e.State == EntityState.Modified ||
                e.State == EntityState.Deleted)
            .ToList();

        _changeLogging.LogChanges(entries);
    }
}
