using System.ComponentModel.DataAnnotations;

namespace bookdb.Models;

public class BookAuthor
{
	[ScaffoldColumn(false)]
	public int Id { get; set; }
	public string? Name { get; set; }
	public ICollection<Book> Books { get; set; }
}
