using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookApp.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Cover { get; set; }
    }

    public class BookDBContext : DbContext
    {
        public DbSet<Book> Books { get; set;}
    }
}