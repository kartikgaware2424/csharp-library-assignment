using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryConsoleApp
{
    class Program
    {
        static List<Book> books = new List<Book>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. Display all books");
                Console.WriteLine("3. Search books by title");
                Console.WriteLine("4. List available books");
                Console.WriteLine("5. Mark a book as borrowed");
                Console.WriteLine("6. Mark a book as returned");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddBook(); break;
                    case "2": DisplayBooks(); break;
                    case "3": SearchByTitle(); break;
                    case "4": ListAvailableBooks(); break;
                    case "5": BorrowBook(); break;
                    case "6": ReturnBook(); break;
                    case "7": return;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            Console.Write("Enter publication year: ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year < 1000 || year > DateTime.Now.Year)
            {
                Console.WriteLine("Invalid year. Book not added.");
                return;
            }

            books.Add(new Book { BookId = nextId++, Title = title, Author = author, PublicationYear = year });
            Console.WriteLine("Book added successfully.");
        }

        static void DisplayBooks()
        {
            if (!books.Any()) Console.WriteLine("No books in the library.");
            else
                foreach (var b in books)
                    Console.WriteLine($"{b.BookId}. {b.Title} by {b.Author}, {b.PublicationYear} - Available: {b.IsAvailable}");
        }

        static void SearchByTitle()
        {
            Console.Write("Enter title to search: ");
            string keyword = Console.ReadLine().ToLower();

            var found = books.Where(b => b.Title.ToLower().Contains(keyword)).ToList();
            if (!found.Any()) Console.WriteLine("No books found.");
            else
                foreach (var b in found)
                    Console.WriteLine($"{b.BookId}. {b.Title} by {b.Author} - Available: {b.IsAvailable}");
        }

        static void ListAvailableBooks()
        {
            var available = books.Where(b => b.IsAvailable).ToList();
            if (!available.Any()) Console.WriteLine("No available books.");
            else
                foreach (var b in available)
                    Console.WriteLine($"{b.BookId}. {b.Title} by {b.Author}");
        }

        static void BorrowBook()
        {
            Console.Write("Enter Book ID to borrow: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = books.FirstOrDefault(b => b.BookId == id);
                if (book == null) Console.WriteLine("Book not found.");
                else if (!book.IsAvailable) Console.WriteLine("Book is already borrowed.");
                else
                {
                    book.IsAvailable = false;
                    Console.WriteLine("Book marked as borrowed.");
                }
            }
            else Console.WriteLine("Invalid input.");
        }

        static void ReturnBook()
        {
            Console.Write("Enter Book ID to return: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var book = books.FirstOrDefault(b => b.BookId == id);
                if (book == null) Console.WriteLine("Book not found.");
                else if (book.IsAvailable) Console.WriteLine("Book is already available.");
                else
                {
                    book.IsAvailable = true;
                    Console.WriteLine("Book marked as returned.");
                }
            }
            else Console.WriteLine("Invalid input.");
        }
    }
}
