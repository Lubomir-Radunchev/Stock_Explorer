using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_Explorer.Models
{
    public class StockRecord
    {
        [Key]
        public int Id { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }
        public string Adj { get; set; }

        [ForeignKey(nameof(StockId))]
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public string Data { get; set; }

    }
}
