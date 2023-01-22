using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
    public class BookSearchViewModel
    {
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }

        [Display(Name = "Show Available Books Only")]
        public bool Available { get; set; }
        [Display(Name = "Book Type")]
        public int? BookTypeId { get; set; }
        public IEnumerable<BookType> BookTypes { get; set; }
        public IEnumerable<Book> Books { get; set; }


    }
}