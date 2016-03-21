﻿using Autofac;
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
            builder.Register<ISessionFactory>(x => NHibernateHelper.GetSession());
            builder.RegisterType<UnitOfWork>();
            builder.RegisterType<GetList>().As<IGetList>();
            builder.RegisterType<Player>().As<IPlayer>();
            builder.RegisterType<PostPlayer>().As<IPostPlayer>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //builder.RegisterWebAiControllerspiModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterWebApiModelBinderProvider();

            container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return container;
        }
    }
}