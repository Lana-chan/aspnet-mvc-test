using System.ComponentModel.DataAnnotations;

namespace bookdb.Models;

public class Book
{
	[ScaffoldColumn(false)]
	public int Id { get; set; }
	public string? Title { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
	public DateTime ReleaseDate { get; set; }
	[Required]
	public int AuthorId { get; set; }
	public BookAuthor Author { get; set; }
}
