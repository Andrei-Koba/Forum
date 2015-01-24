using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Implementation;
using Dal.Interface.Entities;
using Dal.Interface.DataAccess;
using System.Data.Entity.Infrastructure;
using Dal.Interface;

namespace Dal.Implementation.Concrete
{
    public class PostRepository :EntityRepository<DalPost>, IPostRepository
    {
        public PostRepository(DbContext context) : base(context) { }
    }
}
