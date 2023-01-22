using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;
        public AdminController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Index()
        {
            var users = _context.Users.ToList();


            List<UserAndRoleViewModel> model = new List<UserAndRoleViewModel>();
            foreach (var user in users)
            {
                var UserAndRoleViewModel = new UserAndRoleViewModel
                {
                    User = user,
                    Roles = UserManager.GetRoles(user.Id).ToList()
                };
                model.Add(UserAndRoleViewModel);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            ViewBag.Userid = id;
            var user = _context.Users.SingleOrDefault(x => x.Id == id);
            ViewBag.Username = user.UserName;
            List<ManageUserRolesViewModel> model = new List<ManageUserRolesViewModel>();
            foreach (var role in _context.Roles.ToList())
            {
                var ManageUserRolesViewModel = new ManageUserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if (UserManager.IsInRole(id, role.Name))
                {
                    ManageUserRolesViewModel.isSelected = true;
                }
                else
                {
                    ManageUserRolesViewModel.isSelected = false;
                }
                model.Add(ManageUserRolesViewModel);
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(string userId, List<ManageUserRolesViewModel> model)
        {

            var currentRoles = UserManager.GetRoles(userId).ToArray();
            UserManager.RemoveFromRoles(userId, currentRoles);

            var selectedRoles = model.Where(x => x.isSelected).Select(x => x.RoleName);
            foreach(var role in selectedRoles)
            {
                var result = UserManager.AddToRole(userId, role);
                if (!result.Succeeded)
                {
                    ViewBag.ErrorMessage = "Failed to edit roles";
                    return View(model);
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(string id)
        {

            ApplicationUser user = UserManager.FindById(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User not found";
            }
            var result = UserManager.Delete(user);
            if (!result.Succeeded)
            {
                ViewBag.ErrorMessage = $"Failed to delete user";
            }
            return RedirectToAction("Index");
        }

    }


}