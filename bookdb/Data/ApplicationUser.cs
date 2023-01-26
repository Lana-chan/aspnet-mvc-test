using Microsoft.AspNetCore.Identity;
using bookdb.Models;

namespace ASPNetCoreIdentityCustomFields.Data
{
	public class ApplicationUser : IdentityUser
	{
		public ICollection<Book> OwnedBooks { get; set; }
		public ICollection<Book> WantedBooks { get; set; }
	}
}
