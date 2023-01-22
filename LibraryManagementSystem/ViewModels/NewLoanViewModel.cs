using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
    public class NewLoanViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        [Required]
        public Reader Reader { get; set; }
        [Required(ErrorMessage = "Please select a book")]
        [Display(Name = "Book Name")]
        public int RentedBookId { get; set; }
    }
}