using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
    public class ReaderSearchViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<Reader> Readers { get; set; }
        [Display(Name = "Has Current Loan")]
        public bool currentlyBorrowedBook { get; set; }

    }
}