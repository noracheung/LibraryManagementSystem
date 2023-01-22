using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class BookLoan
    {
        public int Id { get; set; }
        [Required]
        public int ReaderId { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public Reader Reader { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BookLoan()
        {
            BorrowDate = DateTime.Now;
            DueDate = DateTime.Now.Add(new TimeSpan(14, 0, 0, 0));
        }
    }
}