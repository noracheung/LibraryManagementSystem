using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models;
using System.Data.Entity;
using LibraryManagementSystem.ViewModels;
using System.Net.Mail;

namespace LibraryManagementSystem.Controllers
{
    [Authorize(Roles = Roles.Admin + "," + Roles.Librarian)]
    public class LoansController : Controller
    {
        // GET: Loans
        private ApplicationDbContext _context;
        public LoansController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var readerList = GetReadersWithLoanRecords();
            return View("Index", readerList);
        }

        public ViewResult Borrow(int id)
        {
            var r = _context.Readers.ToList().FirstOrDefault(x => x.Id == id);
            var books = _context.Books.Include(c => c.Type).ToList();
            var viewModel = new NewLoanViewModel()
            {
                Reader = r,
                Books = books
            };
            
            ViewBag.Message = "New Loan";
            return View("LoanForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NewLoanViewModel loan)
        {
            var rentedBook = _context.Books.Include(c => c.Type).FirstOrDefault(m => m.Id == loan.RentedBookId);
            //var reader = _context.Readers.ToList().FirstOrDefault(c => c.Id == loan.Reader.Id);
            var reader = GetReadersWithLoanRecords().FirstOrDefault(r =>r.Id == loan.Reader.Id);

            if (reader.BookLoans.Count() >= 5)
            {
                ModelState.AddModelError("RentedBookId", "Each reader can only borrow 5 books. Please return borrowed books before making new loan. ");
            }

            if(rentedBook.NumberInStock <= 0)
            {
                ModelState.AddModelError("RentedBookId", $"All \"{rentedBook.Name}\" are rented out");
            }

            if (!ModelState.IsValid || rentedBook.NumberInStock <= 0)
            {
                var books = _context.Books.ToList();
                var viewModel = new NewLoanViewModel()
                {
                    Reader = reader,
                    Books = books,
                };
                ViewBag.Message = "Validation Error!";
                
                return View("LoanForm", viewModel);
            }

            rentedBook.NumberInStock--;

            var newLoan = new BookLoan()
            {
                ReaderId = loan.Reader.Id,
                Reader = reader,
                BookId = loan.RentedBookId,
                Book = rentedBook,
                ReturnDate = null
            };

            _context.BookLoans.Add(newLoan);

            _context.SaveChanges();
            return RedirectToAction("Index", "Loans");
        }

        public ActionResult Return(int id)
        {
            var loan = _context.BookLoans.FirstOrDefault(c => c.Id == id);
            if (loan == null)
                return HttpNotFound();

            loan.ReturnDate = DateTime.Now;
            var returnBook = _context.Books.Include(m => m.Type).FirstOrDefault(c => c.Id == loan.BookId);

            SendEmail(loan.BookId);

            returnBook.NumberInStock++;
            _context.SaveChanges();
            
            var readerList = GetReadersWithLoanRecords();
            return View("Index", readerList);
        }



        private List<Reader> GetReadersWithLoanRecords()
        {
            var readers = _context.Readers.ToList();
            foreach (var reader in readers)
            {
                reader.BookLoans = _context.BookLoans.Include(b => b.Book)
                                     .Where(r => r.ReaderId == reader.Id)
                                     .Where(r => r.ReturnDate == null).ToList();
            }
            return readers;
        }

        private void SendEmail(int id)
        {
            var requestedEmails = _context.EmailNotifications.Where(b => b.BookId == id).Where(b => b.SendEmailDate == null).ToList();

            if (requestedEmails == null) return;
            
            foreach(var request in requestedEmails)
            {
                var mailMessage = new MailMessage()
                {
                    Subject = "The book you are looking for is now available!",
                    Body = $"The book {request.BookName} is now available in library. " +
                            $"Please note that the book is not reserved for you. You may check the updated status of the book in the system",
                    From = new MailAddress("meicheung0221@gmail.com"),
                    //IsBodyHtml = true
                };
                mailMessage.To.Add(request.Email);

                //config in web.config <systemnet>
                var smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);
                request.SendEmailDate = DateTime.Now;
                _context.SaveChanges();
            }

        }

    }
}