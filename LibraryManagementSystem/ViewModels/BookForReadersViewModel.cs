using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
    public class BookForReadersViewModel
    {
        public Book Book { get; set; }
        public List<Reader> Readers { get; set; }
    
    }
}