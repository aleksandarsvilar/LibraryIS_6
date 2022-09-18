using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryIS.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Genre")]
        public string Name { get; set; }
    }
}
