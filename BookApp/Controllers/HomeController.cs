using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SQLite;
using BookApp.Models;
using System.Data;

namespace BookApp.Controllers
{
    public class HomeController : Controller
    {
        public List<Book> Books { get; set; }

        // GET: Home
        public ActionResult List()
        {
            //var connstring = "Data Source=booksdb;mode=memory;cache=shared";
            var connstring = "FullUri=file:booksdb?mode=memory&cache=shared";

            using (var conn1 = new SQLiteConnection(connstring))
            {
                conn1.Open();

                // Create a table
                using (var command = new SQLiteCommand("CREATE TABLE Books (id INTEGER PRIMARY KEY, Title varchar(100), Author varchar(50), Year int, Cover varchar(10))", conn1))
                {
                    command.ExecuteNonQuery();
                }

                // Insert some book data
                using (var command = new SQLiteCommand("INSERT INTO Books (Title, Author, Year, Cover) VALUES " +
                                                        "('Test Book 1', 'Test Author', 2024, 'Hardback')," +
                                                        "('Test Book 2', 'Another Author', 2020, 'Paperback')", conn1))
                {
                    command.ExecuteNonQuery();
                }

                var listOfBooks = new List<Book>();
                // Query the data
                using (var command = new SQLiteCommand("SELECT * FROM Books", conn1))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfBooks.Add(new Book
                        {
                            ID = int.Parse(reader["id"].ToString()),
                            Title = reader["title"].ToString(),
                            Author = reader["author"].ToString(),
                            Year = int.Parse(reader["year"].ToString()),
                            Cover = reader["Cover"].ToString()
                        });
                    }
                    reader.Close();
                    Books = listOfBooks;
                 }
            }

            return View(Books);
        }

        public ActionResult Details(int BookID)
        {
            return View(Books.Find(x => x.ID == BookID));
        }

    }
}