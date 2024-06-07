using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShippingCalculator.Models
{
    public class Carrier
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }  // Nullable
        
        public decimal MaxLength { get; set; }
        public decimal MaxWidth { get; set; }
        public decimal MaxHeight { get; set; }
        public decimal MaxWeight { get; set; }
        public decimal MaxCubic { get; set; }
        
        public ICollection<PostalCode>? SupportedPostalCodes { get; set; } = new List<PostalCode>();  // Nullable
    }
}
