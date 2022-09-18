using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Models.ViewModels
{
    public class LoanVM
    {
        public Loan Loan { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MembersList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> BooksList { get; set; }
    }
}
