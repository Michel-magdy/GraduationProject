using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IUser : IService<User>
{
    List<User> GetUsers();
}
