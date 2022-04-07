using UserApi.Model;

namespace UserApi.Data;

public interface IUserRepository
{
    User AddUser(User user);
    IEnumerable<User> GetUsers();
    User PutUser(User user);
    User GetUserById(int id);
    bool DeleteUser(int id);
}