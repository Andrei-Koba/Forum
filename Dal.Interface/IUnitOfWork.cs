using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Dal.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
