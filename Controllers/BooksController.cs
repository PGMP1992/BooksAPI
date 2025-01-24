using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Make sure this namespace is included
using System.Security.Claims;
using EndProj_Books.Models;
using EndProj_Books.Services;
using EndProj___Books.Data;

namespace EndProj_Books.Controllers
{
    public class BooksController : Controller
    {
        private readonly BooksContext _context;
        private readonly BookService _bookService;
        private readonly ILogger<BooksController> _logger; // Add this line

        public BooksController(BooksContext context, BookService bookService, ILogger<BooksController> logger) // Modify constructor
        {
            _context = context;
            _bookService = bookService;
            _logger = logger; // Initialize logger
        }

        // GET: Books
        public async Task<IActionResult> Index(string sortOrder)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";
            ViewBag.CompleteSortParm = sortOrder == "Complete" ? "complete_desc" : "Complete";

            var books = from b in _context.Books
                        where b.UserId == userId
                        select b;

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title);
                    break;
                case "Author":
                    books = books.OrderBy(b => b.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author);
                    break;
                case "Complete":
                    books = books.OrderBy(b => b.Complete);
                    break;
                case "complete_desc":
                    books = books.OrderByDescending(b => b.Complete);
                    break;
                default:
                    books = books.OrderBy(b => b.Title);
                    break;
            }

            return View(await books.ToListAsync());
        }

        // GET: Books/AddByISBN
        public IActionResult AddByISBN()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                ModelState.AddModelError("", "ISBN is required.");
                return View();
            }

            var bookDetails = await _bookService.GetBookDetailsByISBNAsync(isbn);

            if (bookDetails == null || string.IsNullOrEmpty(bookDetails.Title))
            {
                ModelState.AddModelError("", "Book not found.");
                return View();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            DateTime? publishDate = null;
            if (DateTime.TryParse(bookDetails.PublishDate, out var parsedDate))
            {
                publishDate = parsedDate;
            }

            var book = new Books
            {
                Title = bookDetails.Title,
                Author = bookDetails.Author,
                Genre = bookDetails.Genre,
                Pages = bookDetails.Pages,
                PublishDate = publishDate,
                ISBN = isbn,
                UserId = userId,
                Complete = false, // Initialize Complete to false,
                Cover = bookDetails.Cover // Added PM
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Genre,Cover,Pages,PublishDate,ISBN,Complete")] Books book)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                book.UserId = userId;

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,Cover,Pages,PublishDate,ISBN,Complete")] Books book)
        {
            _logger.LogInformation("Entering Edit action method. Initial ModelState.IsValid: {IsValid}", ModelState.IsValid);

            if (id != book.Id)
            {
                return NotFound();
            }

            // Set UserId from the current user's claims
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            book.UserId = userId;

            // Remove any existing ModelState errors for UserId and User
            ModelState.Remove("UserId");
            ModelState.Remove("User");

            // Re-validate the model state now that UserId is set
            TryValidateModel(book);

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError("Validation error: {ErrorMessage}", error.ErrorMessage);
                    }
                }
                _logger.LogWarning("ModelState is not valid after setting UserId. Errors: {Errors}", 
                    ModelState.Values.SelectMany(v => v.Errors));
                return View(book);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating book with ID {Id}. Complete: {Complete}", book.Id, book.Complete);
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Book with ID {Id} updated successfully.", book.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("ModelState is not valid. Errors: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
