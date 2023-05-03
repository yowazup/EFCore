using Microsoft.EntityFrameworkCore;
using EFCore.Models;
using EFCore.Repositories;

namespace EFCore
{
    /// <summary>
    /// Задание 25.5.4 находится в BookRepository в конце
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var user1 = new User { Name = "Anton" };
            var user2 = new User { Name = "Nikita" };

            //Добавили пользователей в БД
            UserRepository.Add(user1);
            UserRepository.Add(user2);

            var book1 = new Book { Name = "Faust" };
            var book2 = new Book { Name = "Live, love, laugh" };
            var book3 = new Book { Name = "Mr. President" };
            var book4 = new Book { Name = "Dota2" };

            //Добавили книги в БД
            BookRepository.Add(book1);
            BookRepository.Add(book2);
            BookRepository.Add(book3);
            BookRepository.Add(book4);

            //Выдали книги первому пользователю
            BookRepository.GiveBook(book1.Id, user1.Id);
            BookRepository.GiveBook(book2.Id, user1.Id);

            //Выдали книги второму пользователю
            BookRepository.GiveBook(book4.Id, user2.Id);

            //Добавили книге автора и жанр
            BookRepository.Update(book4.Id, null, "Nikita", "Gaming");
        }
    }
}



