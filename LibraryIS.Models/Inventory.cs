using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryIS.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Stock number")]
        [Required]
        public int StockNumber { get; set; } = 0;

        [DisplayName("Shelf number")]
        [Required]
        public int ShelfNumber { get; set; } = 0;

        [Required]
        public bool IsAvailable { get; set; } = true;

        public Book Book { get; set; }
    }
}
