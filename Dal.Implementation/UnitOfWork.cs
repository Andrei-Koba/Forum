using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Interface;
using System.Data.Entity;

namespace Dal.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.Dispose();
                throw new Exception("Db context is unavalable", e);
            }
        }

        public virtual void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }
}
