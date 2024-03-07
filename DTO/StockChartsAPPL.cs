using System.ComponentModel.DataAnnotations;

namespace Stock_Explorer.DTO
{
    public class StockChartsAPPL
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public List<string> Attributes = new List<string>();

    }
}
