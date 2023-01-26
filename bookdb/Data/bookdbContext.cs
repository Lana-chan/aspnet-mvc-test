using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bookdb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ASPNetCoreIdentityCustomFields.Data;

namespace bookdb.Data
{
	public class bookdbContext : IdentityDbContext<ApplicationUser>
	{
		public bookdbContext (DbContextOptions<bookdbContext> options)
			: base(options)
		{
		}

		public DbSet<bookdb.Models.Book> Book { get; set; } = default!;

		public DbSet<bookdb.Models.BookAuthor> BookAuthor { get; set; } = default!;

		// get an author or create new one by passing author name
		public BookAuthor FindOrCreateAuthor(string AuthorName)
		{
			var author = BookAuthor.FirstOrDefault(i => i.Name == AuthorName);

			if (author == null)
			{
				author = new BookAuthor{Name = AuthorName};
				BookAuthor.Add(author);
				SaveChanges();
			}

			return author;
		}
	}
}
