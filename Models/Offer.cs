using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShippingCalculator.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public int CarrierId { get; set; }

        [Required]
        public Carrier Carrier { get; set; }

        [Required]
        public string OfferType { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum Weight must be a positive number.")]
        public decimal MinimumWeight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Maximum Weight must be a positive number.")]
        public decimal MaximumWeight { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Base Cost must be a positive number.")]
        public decimal BaseCost { get; set; }

        public decimal? ExtraCostPerKg { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimum Shipping Cost must be a positive number.")]
        public decimal MinimumShippingCost { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cubic Meter Cost must be a positive number.")]
        public decimal CubicMeterCost { get; set; }

        public List<PostalCodeOffer> PostalCodeOffers { get; set; } = new List<PostalCodeOffer>();
    }
}
