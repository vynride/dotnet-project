using UserManagementAPI.Models;

namespace UserManagementAPI.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User? GetById(int id);
    User Create(User user);
    bool Update(int id, User user);
    bool Delete(int id);
    bool EmailExists(string email, int? exceptUserId = null);
}
