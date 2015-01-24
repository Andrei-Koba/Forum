using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Web.Common;
using Dal.Interface;
using Dal.Implementation;
using System.Data.Entity;
using Bll.Implementation.Services;
using Bll.Interface.DataServices;
using Dal.Implementation.Concrete;
using Dal.Interface.DataAccess;
using Bll.Implementation.EntityMappers;
using Bll.Interface.Entities;
using Dal.Interface.Entities;


namespace DependencyResolver
{
    public static class ResolverConfiguration
    {
        public static void Configure(this IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<DbContext>().To<EntitiesContext>().InSingletonScope();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<ITopicRepository>().To<TopicRepository>();
            kernel.Bind<IPostRepository>().To<PostRepository>();

            kernel.Bind<IEntityMapper<User, DalUser>>().To<UserMapper>();
            kernel.Bind<IEntityMapper<Role, DalRole>>().To<RoleMapper>();
            kernel.Bind<IEntityMapper<Topic, DalTopic>>().To<TopicMapper>();
            kernel.Bind<IEntityMapper<Post, DalPost>>().To<PostMapper>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ITopicService>().To<TopicService>();
            kernel.Bind<IPostService>().To<PostService>();

        }
    }
}
