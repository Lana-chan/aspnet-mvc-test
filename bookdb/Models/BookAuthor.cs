using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bookdb.Models;

public class BookAuthor
{
	[ScaffoldColumn(false)]
	public int Id { get; set; }
	public string? Name { get; set; }
	[ValidateNever]
	public ICollection<Book> Books { get; set; }
}
