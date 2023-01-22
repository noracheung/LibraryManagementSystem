using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryManagementSystem.ViewModels
{
    public class UserAndRoleViewModel
    {
        public ApplicationUser User { get; set; }
        public List<string> Roles { get; set; }
    }
}