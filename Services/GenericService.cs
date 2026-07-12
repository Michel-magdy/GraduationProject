using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class GenericService<TEntity> : IService<TEntity> where TEntity : class
{
    private readonly Context context;
    private readonly DbSet<TEntity> entity;

    public GenericService(Context _context)
    {
        context = _context;
        entity = context.Set<TEntity>();
    }

    public void Add(TEntity _entity)
    {
        this.entity.Add(_entity);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var obj = GetById(id);

        if (obj == null)
        {
            return;
        }

        if (obj is ISoftDelete softDelete)
        {
            softDelete.IsDeleted = true;
            context.Update(obj);
        }
        else
        {
            entity.Remove(obj);
        }

        context.SaveChanges();
    }

    public List<TEntity> GetAll()
    {
        if (typeof(ISoftDelete).IsAssignableFrom(typeof(TEntity)))
        {
            return entity
                .Where(entity => !EF.Property<bool>(entity, nameof(ISoftDelete.IsDeleted)))
                .ToList();
        }

        return entity
            .ToList();
    }

    public TEntity? GetById(int id)
    {
        return entity.Find(id);
    }

    public void Update(TEntity _entity)
    {
        entity.Update(_entity);
        context.SaveChanges();
    }
}
