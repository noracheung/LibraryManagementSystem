using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter Author")]
        [StringLength(100)]
        public string Author { get; set; }
        [Required(ErrorMessage = "Please enter ISBN")]
        [StringLength(13, ErrorMessage ="ISBN length is 13")]
        public string ISBN { get; set; }
        [Display(Name = "Number In Stock")]
        public int NumberInStock { get; set; }
        public BookType Type { get; set; }
        [Display(Name = "Book Type")]
        [Required(ErrorMessage = "Please enter Book Type")]
        public int BookTypeId { get; set; }
        public string FilePath { get; set; }
    }
}