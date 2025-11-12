namespace Pacagroup.Ecommerce.Infraestructura.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<User> Users { get; set; } = default!;

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

            // CompanyName NOT NULL, por ejemplo
            entity.Property(e => e.CompanyName)
                  .IsRequired()
                  .HasMaxLength(40);

            // 👇 MUY IMPORTANTE: EF debe ignorar CustomerId
            entity.Ignore(e => e.CustomerId);
        });

        base.OnModelCreating(modelBuilder);
    }
}
