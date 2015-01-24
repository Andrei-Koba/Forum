using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces;

namespace Bll.Implementation.EntityMappers
{
    public interface IEntityMapper<TBll, TDal>
        where TBll : IEntity
        where TDal : IEntity
    {
        TBll GetBll(TDal dalEntity);
        TDal GetDal(TBll bllEntity);
    }
}
