using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IMS.Plugins.EfCoreSQLite;

public class ImsDbContext : DbContext
{
    public ImsDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Inventory>? Inventories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<InventoryTransaction>? InventoryTransactions { get; set; }
    public DbSet<ProductTransaction>? ProductTransactions { get; set; }
    public DbSet<ProductInventory>? ProductInventories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInventory>()
            .HasKey(pi => new { pi.ProductId, pi.InventoryId });
        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Product)
            .WithMany(p => p.ProductInventories)
            .HasForeignKey(pi => pi.ProductId);
        modelBuilder.Entity<ProductInventory>()
            .HasOne(pi => pi.Inventory)
            .WithMany(i => i.ProductInventories)
            .HasForeignKey(pi => pi.InventoryId);

        modelBuilder.Entity<Inventory>().HasData(
            new() { Id = 1, Name = "Bike Seat", Quantity = 10, Price = 2 },
            new() { Id = 2, Name = "Bike Body", Quantity = 10, Price = 15 },
            new() { Id = 3, Name = "Bike Wheels", Quantity = 20, Price = 8 },
            new() { Id = 4, Name = "Bike Pedels", Quantity = 20, Price = 1 }
        );

        modelBuilder.Entity<Product>().HasData(
            new() { Id = 1, Name = "Bike", Quantity = 10, Price = 150 },
            new() { Id = 2, Name = "Car", Quantity = 5, Price = 25000 }
        );

        modelBuilder.Entity<ProductInventory>().HasData(
            new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuality = 1 }, // seat
            new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuality = 1 }, // body
            new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuality = 2 }, // wheels
            new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuality = 2 } // pedals
        );
    }
}

public class ImsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<ImsDbContext>
{
    public ImsDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ImsDbContext>();
        builder.UseSqlite("DataSource=..\\..\\Data\\Development_design.db");
        return new ImsDbContext(builder.Options);
    }
}