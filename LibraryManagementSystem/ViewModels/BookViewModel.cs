using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
    public class BookViewModel
    {
        public IEnumerable<BookType> BookTypes { get; set; }
        public Book Book { get; set; }
        public HttpPostedFileBase Image { get; set; }

    }
}