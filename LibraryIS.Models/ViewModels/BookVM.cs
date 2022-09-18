﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryIS.Models.ViewModels
{
    public class BookVM
    {
        public Book Book { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> AuthorList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> GenreList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> PublisherList { get; set; }
    }
}
