using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class UpdateStockRequest
    {

        [Required]
        [MaxLength(10, ErrorMessage = "Symbol can't be more than 10 characters")]
        [MinLength(1, ErrorMessage = "Symbol must be at least 1 characters")]


        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(50, ErrorMessage = "Company Name can't be more than 50 characters")]
        [MinLength(5, ErrorMessage = "Company Name must be at least 5 characters")]

        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 1000000, ErrorMessage = "Purchase must be between 0 and 1000000")]

        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100, ErrorMessage = "Last Div must be between 0 and 1000000")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Industry can't be more than 50 characters")]
        [MinLength(5, ErrorMessage = "Industry must be at least 5 characters")]
        public string Industy { get; set; } = "";
        [Required]
        [Range(1, 1000000000, ErrorMessage = "Market Cap must be between 0 and 1000000000")]
        public long MarketCap { get; set; }

    }
}