using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Models
{
    public class Loan
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("MemberId")]
        [Required]
        public int MemberId { get; set; }
        [ValidateNever]
        public Member Member { get; set; }


        [ForeignKey("InventoryId")]
        [Required]
        public int InventoryId { get; set; }
        [ValidateNever]
        public Inventory Inventory { get; set; }


        [Required]
        public DateTime LoanDate { get; set; } = DateTime.Now;
        

        [Required]
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);
    }
}
