using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;
using System.Linq.Expressions;

namespace Bll.Interface
{
    public interface IService<TEntity>: IDisposable where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(long id);
        void Add(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicates);
        void Save();
        void Dispose();
    }
}
