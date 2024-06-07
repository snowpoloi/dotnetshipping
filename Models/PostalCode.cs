namespace ShippingCalculator.Models
{
    public class PostalCode
    {
        public int Id { get; set; }
        public string? Postal { get; set; }  // Nullable
        public string? Location { get; set; }  // Nullable
        public string? Nomos { get; set; }  // Nullable
    }
}
