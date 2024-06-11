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
        public DbSet<PostalCodeOffer> PostalCodeOffers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostalCodeOffer>()
                .HasKey(pco => new { pco.PostalCodeId, pco.OfferId });

            modelBuilder.Entity<PostalCodeOffer>()
                .HasOne(pco => pco.PostalCode)
                .WithMany(pc => pc.PostalCodeOffers) // Ensure PostalCodeOffers is defined in PostalCode
                .HasForeignKey(pco => pco.PostalCodeId);

            modelBuilder.Entity<PostalCodeOffer>()
                .HasOne(pco => pco.Offer)
                .WithMany(o => o.PostalCodeOffers)
                .HasForeignKey(pco => pco.OfferId);
        }
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
