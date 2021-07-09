using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TCCESTOQUE.Data;

namespace TCCESTOQUE.Repository
{
    public class BaseRepository<T> where T : class
    {
        public readonly TCCESTOQUEContext _context;

        public BaseRepository(TCCESTOQUEContext context)
        {
            _context = context;
        }

        public virtual T GetById(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual DbSet<T> GetContext()
        {
            return _context.Set<T>();
        }

        public virtual T GetOne(Guid? id)
        {
            return _context.Set<T>().FirstOrDefault();
        }

        public void PostCriacao(T model)
        {
            _context.Set<T>().Add(model);
            SaveChanges();
        }

        public virtual T GetEdicao(Guid? id)
        {
            return _context.Set<T>().Find(id);
        }

        public void PutEdicao(T model)
        {
            _context.Set<T>().Update(model);
            SaveChanges();
        }

        public void PostExclusao(T model)
        {
            _context.Set<T>().Remove(model);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
