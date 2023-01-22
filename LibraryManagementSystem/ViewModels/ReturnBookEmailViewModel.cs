using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
    public class ReturnBookEmailViewModel
    {
        public string Email { get; set; }
        public string BookName { get; set; }
        public int BookId { get; set; }

        public ReturnBookEmailViewModel(string email, string bookName, int bookId)
        {
            Email = email;
            BookName = bookName;
            BookId = bookId;
        }
        public ReturnBookEmailViewModel()
        {
                
        }
    }


}