using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using bookdb.Models;

namespace bookdb.Data
{
    public class bookdbContext : DbContext
    {
        public bookdbContext (DbContextOptions<bookdbContext> options)
            : base(options)
        {
        }

        public DbSet<bookdb.Models.Book> Book { get; set; } = default!;

        public DbSet<bookdb.Models.BookAuthor> BookAuthor { get; set; } = default!;
    }
}
