using System.Linq.Expressions;

namespace GraduationProject.Interfaces;

public interface IService<T>
{
    List<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
    IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);

    T? GetById(int id, params Expression<Func<T, object>>[] includes);
}