using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public class OrderContext: DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }
    
    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity
                .HasKey(e => e.Id)
                .HasName("order_pkey");
            
            
            entity
                .ToTable("order");
            

            entity
                .HasIndex(e => e.Id, "order_guid_key")
                .IsUnique();
            
            entity
                .HasIndex(e => e.Number, "order_number")
                .IsUnique();
            
            entity
                .Property(e => e.Id).HasColumnName("id")
                .IsRequired();
            
            entity
                .Property(e => e.Guid).HasColumnName("guid")
                .IsRequired();
            
            entity
                .Property(e => e.Number)
                .HasColumnName("number")
                .IsRequired();
            
            entity
                .Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();
        });
    }
}