using System.ComponentModel.DataAnnotations;

namespace Stock_Explorer.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }

        public Dictionary<string, StockRecord> records = new Dictionary<string, StockRecord>(); 
    }
}

