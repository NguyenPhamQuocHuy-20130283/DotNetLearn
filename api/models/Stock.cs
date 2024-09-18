using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace api.models
{
    [Table("Stocks")]
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }
        public string Industy { get; set; } = "";
        public long MarketCap { get; set; }
        public List<Comment> Comments { get; set; } = [];
        public List<Portfolio> Portfolios { get; set; } = [];

    }
}