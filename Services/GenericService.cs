using GraduationProject.Interfaces;
using GraduationProject.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject.Services;

public class GenericService<TEntity> : IService<TEntity> where TEntity : class
{
    readonly Context context;
    readonly DbSet<TEntity> entity;

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
        var Obj = GetById(id);
        if (Obj != null)
        {
            entity.Remove(Obj);
            context.SaveChanges();
        }
        return;
    }

    public List<TEntity> GetAll()
    {
        return entity.ToList();
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