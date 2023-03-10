using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bookdb.Data;
using bookdb.Models;
using ASPNetCoreIdentityCustomFields.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace bookdb.Controllers
{
	public class WantedBooksController : Controller
	{
		private readonly bookdbContext _context;

		private readonly UserManager<ApplicationUser> _userManager;

		public WantedBooksController(bookdbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: WantedBooks
		[Authorize]
		public async Task<IActionResult> Index()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return NotFound();
			}

			var dbUser = await _context.Users
				.Include(u => u.WantedBooks)
				.ThenInclude(b => b.Author)
				.FirstOrDefaultAsync(u => u.Id == user.Id);

			if (dbUser == null)
			{
				return NotFound();
			}

			return View(dbUser.WantedBooks.ToList());
		}

		// POST: WantedBooks/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Add(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Book book = await _context.Book.FindAsync(id);
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (book == null || user == null)
			{
				return NotFound();
			}

			await _context.Entry(user).Collection(x => x.WantedBooks).LoadAsync();

			if (!user.WantedBooks.Contains(book))
			{
				user.WantedBooks.Add(book);
			}
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
		
		// POST: WantedBooks/Remove
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public async Task<IActionResult> Remove(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Book book = await _context.Book.FindAsync(id);
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (book == null || user == null)
			{
				return NotFound();
			}

			await _context.Entry(user).Collection(x => x.WantedBooks).LoadAsync();

			if (user.WantedBooks.Contains(book))
			{
				user.WantedBooks.Remove(book);
			}
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
