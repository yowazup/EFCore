using EFCore.Models;

namespace EFCore.Repositories
{
    public class UserRepository
    {
        public static void Add(User user)
        {
            using (var db = new AppContext())
            {
                db.Users.Add(user);
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                db.SaveChanges();
            }
        }

        public static void Delete(int userId)
        {
            using (var db = new AppContext())
            {
                var user = SelectOne(userId);
                
                db.Users.Remove(user);
                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Deleted; 
                db.SaveChanges();
            }
        }

        public static void Update(int userId, string userName) 
        {
            using (var db = new AppContext())
            {
                var user = SelectOne(userId);
                user.Name = userName;

                db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static User? SelectOne(int userId) 
        {
            using (var db = new AppContext())
            {
                var user = db.Users.FirstOrDefault(user => user.Id == userId);
                return user;
            }
        }

        public static List<User> SelectAll(int userId)
        {
            using (var db = new AppContext())
            {
                var allUsers = db.Users.ToList();
                return allUsers;
            }
        }
    }
}
