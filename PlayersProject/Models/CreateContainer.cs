using Autofac;
using Autofac.Integration.WebApi;
using NHibernate;
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

        public void InitContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<NHibernateHelper>();            
            builder.RegisterType<UnitOfWork>();
            builder.RegisterType<GetList>().As<IGetList>();
            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return container;
        }
    }
}