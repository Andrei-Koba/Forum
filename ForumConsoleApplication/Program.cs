using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll.Implementation.Services;
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
            IUserService userService = _resolver.Get<IUserService>();
            //IEnumerable<User> users = userService.GetAll();
            //IRoleService roleService = _resolver.Get<IRoleService>();
            //List<Role> roles = roleService.GetAll().ToList();
            IUnitOfWork uow = _resolver.Get<IUnitOfWork>();
            ITopicService topicService = _resolver.Get<ITopicService>();
            User user = userService.GetById(1);
            Topic t1 = new Topic() { Id = 1, CreationDate = DateTime.Now, Creator = user, Name = "Topic1" };
            Topic t2 = new Topic() { Id = 2, CreationDate = DateTime.Now, Creator = user, Name = "Topic2" };
            Topic t3 = new Topic() { Id = 3, CreationDate = DateTime.Now, Creator = user, Name = "Topic3" };
            topicService.Add(t1);
            topicService.Add(t2);
            topicService.Add(t3);
            IEnumerable<Topic> topics = topicService.GetAll();
            foreach (var item in topics)
            {
                Console.WriteLine(item.Name);
            }
            uow.Dispose();
            Console.ReadKey();
        }
    }
}
