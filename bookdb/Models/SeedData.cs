using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using bookdb.Data;
using System;
using System.Linq;

namespace bookdb.Models;

public static class SeedData
{
	public static void Initialize(IServiceProvider serviceProvider)
	{
		using (var context = new bookdbContext(
			serviceProvider.GetRequiredService<
				DbContextOptions<bookdbContext>>()))
		{
			// Populate authors if not found
			if (!context.BookAuthor.Any())
			{
				context.BookAuthor.AddRange(
					new BookAuthor
					{
						Name = "Terry Pratchett"
					},
					new BookAuthor
					{
						Name = "Douglas Adams"
					},
					new BookAuthor
					{
						Name = "Douglas Coupland"
					}
				);
				context.SaveChanges();
			}

			// Populate books if not found
			if (!context.Book.Any())
			{
				context.Book.AddRange(
					new Book
					{
						Title = "Mort",
						ReleaseDate = DateTime.Parse("1987-11-12"),
						AuthorId = context.FindOrCreateAuthor("Terry Pratchett").Id
					},
					new Book
					{
						Title = "Hogfather",
						ReleaseDate = DateTime.Parse("1996-01-01"),
						AuthorId = context.FindOrCreateAuthor("Terry Pratchett").Id
					}
				);
				context.SaveChanges();
			}
		}
	}
}