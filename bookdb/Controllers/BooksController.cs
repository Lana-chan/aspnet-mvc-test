using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bookdb.Data;
using bookdb.Models;
using Microsoft.AspNetCore.Authorization;

namespace bookdb.Controllers
{
    public class BooksController : Controller
    {
        private readonly bookdbContext _context;
        private readonly IConfiguration _config;

        public BooksController(bookdbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bookdbContext = _context.Book.Include(b => b.Author);
            return View(await bookdbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.BookAuthor, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Title,ReleaseDate,AuthorId")] Book book, IFormFile CoverImage)
        {
            ModelState.Remove("CoverImageFile");
            if (ModelState.IsValid)
            {
                if (CoverImage != null && CoverImage.Length > 0) {
                    ReplaceBookCover(book, CoverImage);
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.BookAuthor, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.BookAuthor, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,AuthorId,CoverImage")] Book book, IFormFile CoverImageFile, string? DeleteImage = null)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            ModelState.Remove("DeleteImage");
            ModelState.Remove("CoverImageFile");
            if (ModelState.IsValid)
            {
                try
                {
                    if (CoverImageFile != null && CoverImageFile.Length > 0) {
                        ReplaceBookCover(book, CoverImageFile);
                    } else if (DeleteImage == "on") {
                        ReplaceBookCover(book, null);
                    }
                    _context.Update(book);
                    await _context.SaveChangesAsync();
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
            ViewData["AuthorId"] = new SelectList(_context.BookAuthor, "Id", "Name", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
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
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'bookdbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async void ReplaceBookCover(Book book, IFormFile file)
        {
            // delete old cover from storage
            if (book.CoverImage != null)
            {
                var oldFile = new FileInfo(book.CoverImage);
                if (oldFile.Exists)
                {
                    oldFile.Delete();
                }
            }

            if (file == null)
            {
                book.CoverImage = null;
            }
            else
            {
                var filePath = Path.Combine(_config["UploadedFilesBase"], book.Id.ToString() + Path.GetExtension(file.FileName));

                if (!Path.Exists(Path.GetDirectoryName(filePath)))
                {
                    System.IO.Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                book.CoverImage = filePath;
            }
        }
    }
}
