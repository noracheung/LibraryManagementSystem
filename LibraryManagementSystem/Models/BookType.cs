using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class BookType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Type { get; set; }

    }
}