using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASPNetCoreIdentityCustomFields.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace bookdb.Models;

public class Book
{
	[ScaffoldColumn(false)]
	public int Id { get; set; }
	public string? Title { get; set; }
	[DataType(DataType.Date)]
	[DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = false)]
	public DateTime ReleaseDate { get; set; }
	[Required]
	public int AuthorId { get; set; }
	[ValidateNever]
	public BookAuthor Author { get; set; }

	[ValidateNever]
	public virtual ICollection<ApplicationUser> UsersOwned { get; set; }
	[ValidateNever]
	public virtual ICollection<ApplicationUser> UsersWanted { get; set; }
}
