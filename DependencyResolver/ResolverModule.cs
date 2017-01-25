using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;
using ORM.Initializers;
using DAL.NLog;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }
        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            }

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<UserRepository>();

            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();

            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>();

            kernel.Bind<IBoardService>().To<BoardService>();
            kernel.Bind<ITopicService>().To<TopicService>();
            kernel.Bind<IPostService>().To<PostService>();

            kernel.Bind<IBoardRepository>().To<BoardRepository>();
            kernel.Bind<ITopicRepository>().To<TopicRepository>();
            kernel.Bind<IPostRepository>().To<PostReposytory>();

            kernel.Bind<IForumLogger>().To<ForumLogger>().InSingletonScope();
        }
    }
}

