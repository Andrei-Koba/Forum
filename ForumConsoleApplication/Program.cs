using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Bll.Implementation.Services;
using Bll.Interface.Entities;
using Bll.Interface.DataServices;
using Ninject;
using DependencyResolver;
using Dal.Implementation.Concrete;
using Dal.Interface.DataAccess;
using Bll.Implementation.EntityMappers;
using Dal.Interface;
using Dal.Implementation;
using Dal.Interface.Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace ForumConsoleApplication
{
    class Program
    {

        private readonly static IKernel _resolver;


        static Program()
        {
            _resolver = new StandardKernel();
            _resolver.Configure();
        }

        static void Main(string[] args)
        {
            IRoleService _roles = _resolver.Get<IRoleService>();
            IUserService _users = _resolver.Get<IUserService>();
            ITopicService _topics = _resolver.Get<ITopicService>();
            IPostService _posts = _resolver.Get<IPostService>();
            User user = _users.GetById(2);
            Topic topic = new Topic() { CreationDate = DateTime.Now, Name = "NewTopic", Creator = user};
            _topics.Add(topic);
            Console.WriteLine("end");
        
      }
    }
}
