using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Linq.Expressions;

namespace Dal.Interface.DataAccess
{

    public interface IEntityRepository<T>:IDisposable where T: class, IEntity
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(params Expression<Func<T, bool>>[] predicates);
        T FindById(long id);
        void Add(T item);
        void Edit(T item);
        void Delete(T item);
        void Save();
        void Dispose();

    }
}
