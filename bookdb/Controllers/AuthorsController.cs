using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bookdb.Data;
using bookdb.Models;

namespace bookdb.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly bookdbContext _context;

        public AuthorsController(bookdbContext context)
        {
            _context = context;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
              return _context.BookAuthor != null ? 
                          View(await _context.BookAuthor.ToListAsync()) :
                          Problem("Entity set 'bookdbContext.BookAuthor'  is null.");
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookAuthor == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            return View(bookAuthor);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookAuthor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookAuthor);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookAuthor == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthor.FindAsync(id);
            if (bookAuthor == null)
            {
                return NotFound();
            }
            return View(bookAuthor);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] BookAuthor bookAuthor)
        {
            if (id != bookAuthor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookAuthor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookAuthorExists(bookAuthor.Id))
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
            return View(bookAuthor);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookAuthor == null)
            {
                return NotFound();
            }

            var bookAuthor = await _context.BookAuthor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookAuthor == null)
            {
                return NotFound();
            }

            return View(bookAuthor);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookAuthor == null)
            {
                return Problem("Entity set 'bookdbContext.BookAuthor'  is null.");
            }
            var bookAuthor = await _context.BookAuthor.FindAsync(id);
            if (bookAuthor != null)
            {
                _context.BookAuthor.Remove(bookAuthor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookAuthorExists(int id)
        {
          return (_context.BookAuthor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
