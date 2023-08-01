using Microsoft.EntityFrameworkCore;
using Category.Api.Models;
namespace Category.Api.Db;

public class CategoryDbContext:DbContext
{
  public CategoryDbContext(DbContextOptions<CategoryDbContext> options) : base(options)
  {
  }

  public DbSet<Category.Api.Models.CategoryType> Categories { get; set; }
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    
    modelBuilder.HasSequence<int>("CategoryId").StartsAt(1).HasMin(1).HasMax(int.MaxValue);
    modelBuilder.Entity<Category.Api.Models.CategoryType>().ToTable("Category");
    modelBuilder.Entity<Category.Api.Models.CategoryType>().HasKey(t=>t.Id);
    modelBuilder.Entity<Category.Api.Models.CategoryType>().Property(t => t.Id).UseSequence("CategoryId");
    modelBuilder.Entity<Category.Api.Models.CategoryType>().Property(t => t.Name).HasMaxLength(50);
    modelBuilder.Entity<Category.Api.Models.CategoryType>().HasOne(t => t.Parent).WithMany(t => t.Children)
      .HasForeignKey(t => t.ParentId).IsRequired(false);
    modelBuilder.Entity<Category.Api.Models.CategoryType>().Property(t => t.ParentId).IsConcurrencyToken();
  }
}