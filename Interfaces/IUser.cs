using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IUser : IService<User>
{
    List<User>? GetOwners();
    List<User>? GetAllUsers();

    IEnumerable<User> GetCustomers();

    User? Login(string email, string password);

    User? GetUserWithDetails(int id);

    bool EmailExists(string email);

    void ChangeRole(int userId, int roleId);

}
