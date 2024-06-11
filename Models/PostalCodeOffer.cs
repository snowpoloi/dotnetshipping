namespace ShippingCalculator.Models
{
    public class PostalCodeOffer
    {
        public int PostalCodeId { get; set; }
        public PostalCode PostalCode { get; set; } = null!; // Add null-forgiving operator

        public int OfferId { get; set; }
        public Offer Offer { get; set; } = null!; // Add null-forgiving operator
    }
}
