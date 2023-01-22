using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System.Data.Entity;


namespace LibraryManagementSystem.Controllers
{
    [Authorize]
    public class ReadersController : Controller
    {
        private ApplicationDbContext _context;
        public ReadersController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var model = new ReaderSearchViewModel();
            model.Readers = _context.Readers.ToList();
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
                return View("AdminView", model);

            var userEmail = User.Identity.Name;
            var reader = model.Readers.SingleOrDefault(r => r.Email == userEmail);
            if (reader == null)
                return View("NotAReader");

            reader.BookLoans = _context.BookLoans.Include(b => b.Book)
                                    .Where(r => r.ReaderId == reader.Id).ToList();
            return View("Details", reader);
        }

        [HttpPost]
        public ActionResult Index(ReaderSearchViewModel model)
        {
            var result = ProcessSearchInput(model).ToList();
            model.Readers = result;
            if (User.IsInRole("Admin") || User.IsInRole("Librarian"))
                return View("AdminView", model);

            return View("UserView", model);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public ActionResult Details(int id)
        {
            var reader = _context.Readers.SingleOrDefault(c => c.Id == id);
            if (reader == null)
                return HttpNotFound();
            return View(reader);
        }

        public ActionResult ViewLoanRecord(int id)
        {
            var reader = _context.Readers.SingleOrDefault(c => c.Id == id);
            if (reader == null)
                return HttpNotFound();

            reader.BookLoans = _context.BookLoans.Include(b => b.Book)
                                    .Where(r => r.ReaderId == reader.Id).ToList();

            return View(reader);
        }


        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        public ViewResult NewReader()
        {
            var types = _context.BookTypes.ToList();
            var model = new Reader()
            {
                BookLoans = new List<BookLoan>()
            };
            ViewBag.Message = "New Reader";

            return View("ReaderForm", model);
        }

        

        public ActionResult Edit(int Id)
        {
            var reader = _context.Readers.SingleOrDefault(c => c.Id == Id);
            if (reader == null)
                return HttpNotFound();

            var model = new Reader()
            {
                Name = reader.Name,
                Email = reader.Email,
                Birthdate = reader.Birthdate,
                BookLoans = _context.BookLoans.Include(b => b.Book)
                                    .Where(r => r.ReaderId == reader.Id).ToList()
            };
            ViewBag.Message = "Edit Reader";
            return View("ReaderForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                var model = new Reader();
                return View("ReaderForm", model);
            }

            if (reader.Id == 0)
            {
                _context.Readers.Add(reader);
            }
            else
            {
                var readerInDB = _context.Readers.Single(c => c.Id == reader.Id);
                readerInDB.Name = reader.Name;
                readerInDB.Email = reader.Email;
                readerInDB.Birthdate = reader.Birthdate;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Readers");
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var reader = _context.Readers.SingleOrDefault(c => c.Id == id);
            if (reader == null)
            {
                return HttpNotFound(); //returns standard 404 error
            }
            //otherwise
            _context.Readers.Remove(reader);
            _context.SaveChanges();
            var readers = _context.Readers.ToList();
            return RedirectToAction("Index", readers);
        }
        private IQueryable<Reader> ProcessSearchInput(ReaderSearchViewModel model)
        {
            var result = _context.Readers.AsQueryable();
            result = GetReadersWithLoanRecords(result.ToList()).AsQueryable();
            if (model != null)
            {
                if (!String.IsNullOrEmpty(model.Name))
                    result = result.Where(b => b.Name.ToLower().Contains(model.Name.ToLower()));
                if (!String.IsNullOrEmpty(model.Email))
                    result = result.Where(b => b.Email.ToLower().Contains(model.Email.ToLower()));
                if (model.currentlyBorrowedBook == true)
                    result = result.Where(b => b.BookLoans.Count() > 0);
            }
            return result;
        }
        private List<Reader> GetReadersWithLoanRecords(List<Reader> readerList)
        {
            foreach (var reader in readerList)
            {
                reader.BookLoans = _context.BookLoans.Include(b => b.Book)
                                     .Where(r => r.ReaderId == reader.Id)
                                     .Where(r => r.ReturnDate == null).ToList();
            }
            return readerList;
        }
    }
}