using GraduationProject.Models;

namespace GraduationProject.Interfaces;

public interface ITable : IService<Table>
{
    List<Table> GetTables();
}
