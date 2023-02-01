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
			// Populate books if not found
			if (!context.Book.Any())
			{
				context.Book.AddRange(
					new Book
					{
						Title = "Mort",
						ReleaseDate = DateTime.Parse("1987-11-12"),
						AuthorId = context.FindOrCreateAuthor("Terry Pratchett").Id,
						CoverImage = "Upload\\1.jpg"
					},
					new Book
					{
						Title = "Hogfather",
						ReleaseDate = DateTime.Parse("1996-01-01"),
						AuthorId = context.FindOrCreateAuthor("Terry Pratchett").Id
					},
					new Book
					{
						Title = "Dirk Gently's Hollistic Detective Agency",
						ReleaseDate = DateTime.Parse("1987-01-01"),
						AuthorId = context.FindOrCreateAuthor("Douglas Adams").Id
					},
					new Book
					{
						Title = "Microserfs",
						ReleaseDate = DateTime.Parse("1995-06-01"),
						AuthorId = context.FindOrCreateAuthor("Douglas Coupland").Id
					}
				);
				context.SaveChanges();
			}
		}
	}
}