using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryIS.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }



        [DisplayName("First name")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }



        [DisplayName("Last name")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
    }
}
