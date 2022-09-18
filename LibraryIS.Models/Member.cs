using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("First name")]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last name")]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [Required]
        [DisplayName("Email address")]
        [MaxLength(30)]
        public string EmailAddress { get; set; }

        [Required]
        [DisplayName("Phone number")]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [DisplayName("Registration date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
