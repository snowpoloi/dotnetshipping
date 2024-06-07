using Microsoft.EntityFrameworkCore;

namespace ShippingCalculator.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
		public DbSet<Carrier> Carriers { get; set; }
		public DbSet<PostalCode> PostalCodes { get; set; }

    }

    public class Offer
	{
    public int Id { get; set; }
    public string? Carrier { get; set; }  // Nullable
    public string? OfferType { get; set; }  // Nullable
    public decimal Rate { get; set; }
    public string? Zone { get; set; }  // Nullable
	}

    public class Shipment
    {
        public int Id { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public decimal VolumetricWeight { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
