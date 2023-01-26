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

namespace bookdb.Controllers
{
	public class OwnedBooksController : Controller
	{
		private readonly bookdbContext _context;

		private readonly UserManager<ApplicationUser> _userManager;

		public OwnedBooksController(bookdbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		// GET: OwnedBooks
		public async Task<IActionResult> Index()
		{
			ApplicationUser user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return NotFound();
			}

			return View(user.OwnedBooks);
		}

		// POST: OwnedBooks/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
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

			await _context.Entry(user).Collection(x => x.OwnedBooks).LoadAsync();

			if (!user.OwnedBooks.Contains(book))
			{
				user.OwnedBooks.Add(book);
			}
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
