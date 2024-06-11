using System.Collections.Generic;

namespace ShippingCalculator.Models
{
    public class PostalCode
    {
        public int Id { get; set; }
        public string Postal { get; set; } = string.Empty; // Initialize with empty string
        public string Location { get; set; } = string.Empty; // Initialize with empty string
        public string Nomos { get; set; } = string.Empty; // Initialize with empty string

        public List<PostalCodeOffer> PostalCodeOffers { get; set; } = new List<PostalCodeOffer>(); // Initialize with empty list
    }
}
