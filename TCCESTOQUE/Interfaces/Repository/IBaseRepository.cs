using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace TCCESTOQUE.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {

        public T GetById(Guid id);

        public DbSet<T> GetContext();

        public ICollection<T> GetAll(Guid vendedorId);

        public T GetOne(Guid? id);

        public void PostCriacao(T model);

        public T GetEdicao(Guid? id);

        public void PutEdicao(T model);

        public void PostExclusao(T model);
    }
}
