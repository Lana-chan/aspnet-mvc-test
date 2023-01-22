using System.ComponentModel.DataAnnotations;

namespace bookdb.Models;

public class Book
{
	[ScaffoldColumn(false)]
	public int Id { get; set; }
	public string? Title { get; set; }
	[DataType(DataType.Date)]
	public DateTime ReleaseDate { get; set; }
	public int AuthorId { get; set; }
}
