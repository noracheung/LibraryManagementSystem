using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System.Data.Entity;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private ApplicationDbContext _context;
        public BooksController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        public ActionResult Index()
        {
            var model = new BookSearchViewModel();
            model.Books = _context.Books.Include(b => b.Type).ToList();
            model.BookTypes = _context.BookTypes.ToList();
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
                return View("AdminView", model);

            return View("UserView", model);
        }

        [HttpPost]
        public ActionResult Index(BookSearchViewModel model)
        {
            var result = ProcessSearchInput(model).ToList();
            model.Books = result;
            model.BookTypes = _context.BookTypes.ToList();
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
                return View("AdminView", model);

            return View("UserView", model);
        }

        public ActionResult Details(int id)
        {
            var book = _context.Books.Include(c => c.Type).SingleOrDefault(c => c.Id == id);
            if (book == null)
                return HttpNotFound();
            return View(book);
        }
        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public ViewResult NewBook()
        {
            var types = _context.BookTypes.ToList();
            var viewModel = new BookViewModel()
            {
                BookTypes = types,
                Book = new Book()
            };
            ViewBag.Message = "New Book";

            return View("BookForm", viewModel);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public ActionResult Edit(int Id)
        {
            var types = _context.BookTypes.ToList();
            var book = _context.Books.SingleOrDefault(c => c.Id == Id);
            if (book == null)
                return HttpNotFound();

            var viewModel = new BookViewModel
            {
                BookTypes = types,
                Book = book
            };
            ViewBag.Message = "Edit Book";
            return View("BookForm", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(BookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new BookViewModel
                {
                    Book = model.Book,
                    BookTypes = _context.BookTypes.ToList()
                };
                return View("BookForm", viewModel);
            }

           
            if (hasFile(model.Image))
            {
                var error = CheckFileSizeAndType(model.Image);
                if (error != null)
                {
                    ModelState.AddModelError("Image", error);
                    var viewModel = new BookViewModel
                    {
                        Book = model.Book,
                        BookTypes = _context.BookTypes.ToList()
                    };
                    if (model.Book.Id == 0)
                        ViewBag.Message = "New Book";
                    else
                    {
                        viewModel.Book.FilePath = _context.Books.FirstOrDefault(b => b.Id == model.Book.Id).FilePath;
                        ViewBag.Message = "Edit Book";
                    }

                    return View("BookForm", viewModel);
                }
                string uploadedTime = System.DateTime.Now.ToString("ddMMyyyyHHMMss");
                string uniqueFileName = uploadedTime + "_" + model.Image.FileName;
                string filePath = Path.Combine(Server.MapPath("~/UploadedFiles"), uniqueFileName);
                model.Image.SaveAs(filePath);
                model.Book.FilePath = uniqueFileName;
                
            }

            if (model.Book.Id == 0)
            {
                _context.Books.Add(model.Book);
            }
            else
            {
                var bookInDB = _context.Books.Single(c => c.Id == model.Book.Id);
                bookInDB.Name = model.Book.Name;
                bookInDB.Author = model.Book.Author;
                bookInDB.ISBN = model.Book.ISBN;
                bookInDB.NumberInStock = model.Book.NumberInStock;
                bookInDB.BookTypeId = model.Book.BookTypeId;
                bookInDB.FilePath = model.Book.FilePath;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        private static bool hasFile(HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }
        private static string CheckFileSizeAndType(HttpPostedFileBase file)
        {
            var imageType = file.ContentType.Split('/')[1];
            var size = file.ContentLength;

            if (imageType != "jpeg" && imageType != "png")
                return "Please upload file in jpg or png";

            if (size > 104857600)
                return "Max file size: 100mb";

            return null;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Include(c => c.Type).SingleOrDefault(c => c.Id == id);
            if (book == null)
            {
                return HttpNotFound(); //returns standard 404 error
            }
            //otherwise
            _context.Books.Remove(book);
            _context.SaveChanges();
            var books = _context.Books.Include(c => c.Type).ToList();
            return View("AdminView", books);
        }
        
        [HttpPost]
        public ActionResult SetUpEmailNotification(EmailNotification model)
        {
            model.SetUpDate = DateTime.Now;
            _context.EmailNotifications.Add(model);
            _context.SaveChanges();
            return Content("Email notification has been set up successfully!");
        }

        private IQueryable<Book> ProcessSearchInput(BookSearchViewModel model)
        {
            var result = _context.Books.Include(c => c.Type).AsQueryable();
            if (model != null)
            {
                if (!String.IsNullOrEmpty(model.Name))
                    result = result.Where(b => b.Name.ToLower().Contains(model.Name.ToLower()));
                if (!String.IsNullOrEmpty(model.Author))
                    result = result.Where(b => b.Author.ToLower().Contains(model.Author.ToLower()));
                if (!String.IsNullOrEmpty(model.ISBN))
                    result = result.Where(b => b.ISBN == model.ISBN);
                if (model.Available == true)
                    result = result.Where(b => b.NumberInStock > 0);
                if (model.BookTypeId.HasValue)
                    result = result.Where(b => b.BookTypeId == model.BookTypeId);
            }
            return result;
        }
    }



}
