using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface.DataServices
{
    public interface ITopicService: IService<Topic>
    {
        IEnumerable<Topic> GetByCreator(long creatorId);
        Topic GetByName(string name);
    }
}
