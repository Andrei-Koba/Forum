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
            long id = 1;
            ITopicService topics = _resolver.Get<ITopicService>();
            Topic topic = topics.GetById(id);
            topic.Name = "Name";
            topics.Edit(topic);
            //ITopicRepository topics = _resolver.Get<ITopicRepository>();
            //DalTopic topic = topics.FindById(id);
            //DalTopic topic2 = new DalTopic() { Id = topic.Id, CreationDate = topic.CreationDate, CreatorId = topic.CreatorId, PostsCount = topic.PostsCount, Name = topic.Name };
            //topic2.Name = "ChangedName";
            //topics.Edit(topic2);
            //topics.Save();
            Console.ReadKey();
        }
    }
}
