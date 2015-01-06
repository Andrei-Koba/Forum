using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Bll.Implementation.EntityMappers
{
    public interface IEntityMapper<TEntityOne, TEntityTwo> : IDisposable
        where TEntityOne : IEntity
        where TEntityTwo : IEntity
    {
        TEntityOne GetEntityOne(TEntityTwo dalEntity);
        TEntityTwo GetEntityTwo(TEntityOne bllEntity);
        void Dispose();
    }
}
