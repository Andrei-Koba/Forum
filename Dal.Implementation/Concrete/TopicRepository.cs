using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Dal.Interface.Entities;
using Dal.Implementation;
using Dal.Interface.DataAccess;
using System.Data.Entity.Infrastructure;
using Dal.Interface;

namespace Dal.Implementation.Concrete
{
    public class TopicRepository: EntityRepository<DalTopic>, ITopicRepository
    {
        public TopicRepository(DbContext context) : base(context) { }
    }
}
