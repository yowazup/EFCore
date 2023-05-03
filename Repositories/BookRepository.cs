using EFCore.Models;
using System.Runtime.Intrinsics.X86;

namespace EFCore.Repositories
{
    public class BookRepository
    {
        public static void Add(Book book)
        {
            using (var db = new AppContext())
            {
                db.Books.Add(book);
                db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                db.SaveChanges();
            }
        }

        public static void Delete(int bookId)
        {
            using (var db = new AppContext())
            {
                var book = SelectOne(bookId);

                db.Books.Remove(book);
                db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static void Update(int bookId, int? bookYear, string? bookAuthor, string? bookGenre)
        {
            using (var db = new AppContext())
            {
                var book = SelectOne(bookId);
                book.Year = bookYear;
                book.Author = bookAuthor;
                book.Genre = bookGenre;

                db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static Book? SelectOne(int bookId)
        {
            using (var db = new AppContext())
            {
                var book = db.Books.FirstOrDefault(book => book.Id == bookId);
                return book;
            }
        }

        public static List<Book> SelectAll(int bookId)
        {
            using (var db = new AppContext())
            {
                var allBooks = db.Books.ToList();
                return allBooks;
            }
        }

        public static void GiveBook(int bookId, int userId)
        { 
        using (var db = new AppContext())
            {
                var book = SelectOne(bookId);
                var user = UserRepository.SelectOne(userId);
               
                book.UserId = user.Id;

                db.Entry(book).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }

        //Получать список книг определенного жанра и вышедших между определенными годами
        public static List<Book> SelectBooksYears(int yearStart, int yearFinish)
        {
            using (var db = new AppContext())
            {
                var books = db.Books.Where(book => book.Year >= yearStart).Where(book => book.Year <= yearFinish).ToList();
                return books;
            }
        }

        //Получать количество книг определенного автора в библиотеке
        public static int CountBooksAuthor(string author)
        {
            using (var db = new AppContext())
            {
                var booksNumber = db.Books.Where(book => book.Author == author).Count();
                return booksNumber;
            }
        }

        //Получать количество книг определенного жанра в библиотеке
        public static int CountBooksGenre(string genre)
        {
            using (var db = new AppContext())
            {
                var booksNumber = db.Books.Where(book => book.Genre == genre).Count();
                return booksNumber;
            }
        }

        //Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке
        public static bool CheckAuthorAndName(string author, string name)
        {
            using (var db = new AppContext())
            {
                var check = db.Books.Where(book => book.Author == author).Where(book => book.Name == name).Count();

                if(check > 0)
                {
                    return true;
                }
                return false;
            }
        }

        //Получать булевый флаг о том, есть ли определенная книга на руках у пользователя
        public static bool CheckBookUser(int bookId)
        {
            var book = SelectOne(bookId);

            if (book.UserId == null)
            {
                 return false;
            }
            return true;
        }

        //Получать количество книг на руках у пользователя
        public static int CountBooksUser(int userId)
        {
            using (var db = new AppContext())
            {
                var booksNumber = db.Books.Where(book => book.UserId == userId).Count();
                return booksNumber;
            }
        }

        //Получение последних вышедших книг
        public static List<Book> SelectNewestBooks()
        {
            using (var db = new AppContext())
            {
                var year = db.Books.OrderByDescending(x => x.Year);
                var books = db.Books.Where(book => book.Year == year.First().Year).ToList();
                return books;
            }
        }

        //Получение списка всех книг, отсортированного в алфавитном порядке по названию
        public static List<Book> NameSortedBooks()
        {
            using (var db = new AppContext())
            {
                var books = db.Books.OrderBy(x => x.Name).ToList();
                return books;
            }
        }

        //Получение списка всех книг, отсортированного в порядке убывания года их выхода
        public static List<Book> YearSortedBooks()
        {
            using (var db = new AppContext())
            {
                var books = db.Books.OrderByDescending(x => x.Year).ToList();
                return books;
            }
        }
    }
}
