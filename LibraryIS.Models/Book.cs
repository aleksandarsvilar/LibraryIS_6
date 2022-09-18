using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryIS.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(50)]
        [DisplayName("Title")]
        public string Name { get; set; }

        public string Description { get; set; }


        [DisplayName("Author")]
        [ForeignKey("AuthorId")]
        [Required]
        public int AuthorId { get; set; }
        [ValidateNever]
        public Author Author { get; set; }


        [DisplayName("Genre")]
        [ForeignKey("GenreId")]
        [Required]
        public int GenreId { get; set; }
        [ValidateNever]
        public Genre Genre { get; set; }


        [DisplayName("Publisher")]
        [ForeignKey("PublisherId")]
        [Required]
        public int PublisherId { get; set; }
        [ValidateNever]
        public Publisher Publisher { get; set; }



        [ForeignKey("InventoryId")]
        [Required]
        public int InventoryId { get; set; }
        [ValidateNever]
        public Inventory Inventory { get; set; }


        [Required]
        public DateTime InsertionDate { get; set; } = DateTime.Now;


        [Required]
        [DisplayName("Publication date")]
        public DateTime PublicationDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(50)]
        public string ISBN { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
