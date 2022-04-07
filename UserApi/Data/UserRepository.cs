using UserApi.Model;

namespace UserApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDataContext db;

        public UserRepository(UserDataContext db)
        {
            this.db = db;
        }
        public User AddUser(User user)
        {
            db.User.Add(user);
            db.SaveChanges();
            return user;
        }
        public IEnumerable<User> GetUsers() => db.User.ToList();

        public User PutUser(User user)
        {
            db.User.Update(user);
            db.SaveChanges();
            return db.User.Where(x => x.Id == user.Id).FirstOrDefault();
        }
        public User GetUserById(int Id)
        {
            return db.User.Where(x => x.Id == Id).FirstOrDefault();
        }

        public bool DeleteUser(int id)
        {
            var user = db.User.Find(id);
            db.User.Remove(user);
            return db.SaveChanges() > 0;
        }
    }
}
