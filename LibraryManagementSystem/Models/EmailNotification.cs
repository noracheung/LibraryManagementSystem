using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class EmailNotification
    {
        public int Id { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required]
        public string Email { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public string BookName { get; set; }
        public DateTime SetUpDate { get; set; }
        public DateTime? SendEmailDate { get; set; }

        public EmailNotification(string bookName, int bookId)
        {
            BookName = bookName;
            BookId = bookId;
            //SetUpDate = DateTime.Now --> Cannot set the datetime here, set in controller
            //sql by default use DateTime2 which cause an error
        }
        public EmailNotification()
        {

        }
    }
}