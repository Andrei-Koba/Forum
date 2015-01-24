using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Interface.Entities;

namespace Bll.Interface.DataServices
{
    public interface IPostService: IService<Post>
    {
        IEnumerable<Post> GetByAuthor(long authorId);
        IEnumerable<Post> GetByTopic(long topicId);

    }
}
