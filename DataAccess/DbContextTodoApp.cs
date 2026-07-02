using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.DataAccess;

public class DbContextTodoApp : DbContext
{
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Categories> Categories { get; set; }

    public DbContextTodoApp(DbContextOptions<DbContextTodoApp> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Users>(entity =>
        {
           entity.HasKey(e => e.Id);
           entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
           entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
           entity.Property(e => e.PasswordHash).IsRequired(); 
           entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<Categories>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.HasData(
                new Categories { Id = 1, Name = "Work" },
                new Categories { Id = 2, Name = "Personal" },
                new Categories { Id = 3, Name = "Shopping" },
                new Categories { Id = 4, Name = "Studying" }
            );
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500); 
            
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()"); 

            entity.HasOne(t => t.Category)
                  .WithMany(c => c.Tasks)
                  .HasForeignKey(t => t.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict); 

            entity.HasOne(t => t.User)
                  .WithMany(u => u.Tasks)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade); 
        });
    }
}