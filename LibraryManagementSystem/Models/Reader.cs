using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Reader
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Email")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Display(Name = "Date Of Birth")]
        public DateTime? Birthdate { get; set; }
        public IEnumerable<BookLoan> BookLoans { get; set; }
    }
}