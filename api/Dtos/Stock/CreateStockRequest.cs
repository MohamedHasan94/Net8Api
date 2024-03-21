using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "Symbol must be 5 or more characters")]
        [MaxLength(25, ErrorMessage = "Symbol must be less than 25 characters")]
        public string Symbol { get; set; } = string.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "CompanyName must be 5 or more characters")]
        [MaxLength(64, ErrorMessage = "CompanyName must be less than 64 characters")]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [Range(0.001, double.MaxValue)]
        public decimal Purchase { get; set; }


        [Required]
        [Range(0.001, double.MaxValue)]
        public decimal LastDiv { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Industry must be 5 or more characters")]
        [MaxLength(25, ErrorMessage = "Industry must be less than 25 characters")]
        public string Industry { get; set; } = string.Empty;

        [Required]
        [Range(0.001, double.MaxValue)]
        public long MarketCap { get; set; }
    }
}