using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.DataAccess;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>()
            .HasKey(ug => ug.UserId); // primary key

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithOne(u => u.UserGroup)
            .HasForeignKey<UserGroup>(ug => ug.UserId);

        base.OnModelCreating(modelBuilder);
    }
}