using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NHibernate;
using PlayersProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PlayersProject.Models
{   
    public class CreateContainer
    {
        private IContainer container { get; set; }

        public CreateContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NHibernateHelper>();
            builder.Register(x => NHibernateHelper.GetSession()).As<ISessionFactory>();
            builder.RegisterType<UnitOfWork>();
            builder.RegisterType<GetList>().As<IGetList>();
            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterType<PostPlayer>().As<IPostPlayer>();
            builder.RegisterType<PostList>().As<IPostList>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return container;
        }
    }
}