using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface IRoom : IService<Room>
{
    List<Room> GetRooms();
}
