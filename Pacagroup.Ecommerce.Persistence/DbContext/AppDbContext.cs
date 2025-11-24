using Pacagroup.Ecommerce.Domain.Entities;

namespace Pacagroup.Ecommerce.Persistence.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Discount> Discounts { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");

            // PK = Id (string)
            entity.HasKey(e => e.Id);

            // Id mapeado a la columna CustomerID de la tabla
            entity.Property(e => e.Id)
                  .HasColumnName("CustomerID")
                  .HasMaxLength(5);

            entity.Property(e => e.CompanyName)
                  .IsRequired()
                  .HasMaxLength(40);

            // 👇 MUY IMPORTANTE: EF debe ignorar CustomerId
            entity.Ignore(e => e.CustomerId);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.ToTable("Discounts");

            entity.Property(e => e.CreatedDate)
                  .IsRequired();

            entity.Property(e => e.Porcentage)
                  .IsRequired();

            entity.Property(e => e.Status)
                  .IsRequired();

        });

        base.OnModelCreating(modelBuilder);
    }
}
