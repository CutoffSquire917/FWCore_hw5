

using Microsoft.EntityFrameworkCore;
using System.Collections;
using FWCore_hw5.Models;
using FWCore_hw5.Data;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();

            var author1 = new Author { Name = "George", Surname = "Orwell", Email = "orwell@mail.com"};
            var author2 = new Author { Name = "Jane", Surname = "Austen", Email = "austen@mail.com"};

            var genre1 = new Genre { Name = "Dystopian" };
            var genre2 = new Genre { Name = "Romance" };
            var genre3 = new Genre { Name = "Drama" };

            var books = new List<Book>
            {
            new Book {Title = "1984", Author = author1, Genre = genre1, Price = 9.99m},
            new Book { Title = "Animal Farm", Author = author1, Genre = genre1, Price = 7.49m },
            new Book { Title = "Pride and Prejudice", Author = author2, Genre = genre2, Price = 8.50m },
            new Book { Title = "Emma", Author = author2, Genre = genre3, Price = 7.95m },
            new Book { Title = "Sense and Sensibility", Author = author2, Genre = genre2, Price = 8.20m }
            };

            if (!db.Authors.Any()) db.Authors.AddRange(author1, author2);
            if (!db.Genres.Any()) db.Genres.AddRange(genre1, genre2, genre3);
            if (!db.Books.Any()) db.Books.AddRange(books);
            
            db.SaveChanges();
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            Console.WriteLine("1) Получить количество книг определенного жанра.");
            Console.WriteLine(db.Books.Where(e => e.Genre.Name == "Dystopian").Count());

            Console.WriteLine("2) Получить минимальную цену для книг определенного автора.");
            Console.WriteLine(db.Books.Where(e => e.Author.Name == "George").Min(e => e.Price));

            Console.WriteLine("3) Получить среднюю цену книг в определенном жанре.");
            Console.WriteLine(db.Books.Where(e => e.Genre.Name == "Romance").Average(e => e.Price));

            Console.WriteLine("4) Получить суммарную стоимость всех книг определенного автора.");
            Console.WriteLine(db.Books.Where(e => e.Author.Name == "Jane").Sum(e => e.Price));

            Console.WriteLine("5) Выполнить группировку книг по жанрам.");
            var v = db.Books.GroupBy(e => e.Genre.Name).Select(e => new
            {
                Books = e.ToList(),
                Genre = e.Key
            });
            Console.WriteLine(string.Join("\n", v.Select(e => $"{e.Genre}\n{string.Join("\n", " - " + e.Books)}")));

            Console.WriteLine("6) Выбрать только названия книг определенного жанра.");
            Console.WriteLine(string.Join("\n", db.Books.Where(e => e.Genre.Name == "Dystopian").Select(e => e.Title)));

            Console.WriteLine("7) Выбрать все книги, кроме тех, что относятся к определенному жанру, используя метод Except.");
            Console.WriteLine(string.Join("\n", db.Books.Except(db.Books.Where(e => e.Genre.Name == "Drama"))));

            Console.WriteLine("8) Объединить книги от двух авторов, используя метод Union.");
            Console.WriteLine(string.Join("\n", db.Books.Where(e => e.Author.Name == "George").Union(db.Books.Where(e => e.Author.Name == "Jane"))));

            Console.WriteLine("9) Достать 5 - ть самых дорогих книг.");
            Console.WriteLine(string.Join("\n", db.Books.OrderBy(e => e.Price).Take(5)));

            Console.WriteLine("10) Пропустить первые 10 книг и взять следующие 5.");
            Console.WriteLine(string.Join("\n", db.Books.Skip(10).Take(5)));

            
        }

    }
}










