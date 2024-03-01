using System.ComponentModel.DataAnnotations;

namespace Stock_Explorer.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime Date { get; set; }
        [Required]
        public double Open { get; set; }
        [Required]
        public double High { get; set; }
        [Required]
        public double Low { get; set; }
        [Required]
        public double Close { get; set; }
        [Required]
        public double AdjClose { get; set; }

        [Required]
        public double Volume { get; set; }

    }
}
