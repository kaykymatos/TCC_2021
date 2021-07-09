using System;
using System.Collections.Generic;

namespace TCCESTOQUE.Interfaces.Service
{
    public interface IBaseService<T> where T : class
    {
        public T GetOne(Guid? id);

        public ICollection<T> GetAll(Guid id);
    }
}
