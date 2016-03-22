using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;

        public static void CreateSessionFactory()
        {
            var cfg = new Configuration();
            cfg.Configure();

            var config =
                Fluently.Configure(cfg)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Startup>())
                .ExposeConfiguration(BuildSchema)                
                .BuildSessionFactory();            

            sessionFactory = config;
        }


        public static void BuildSchema(Configuration cfg)
        {
            // delete the existing db on each run
            //if (File.Exists(DBstring))
            //    File.Delete(DBstring);

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(cfg)
              .Create(false, true);

            //new SchemaUpdate(cfg)
            //    .Execute(false, true);
        }
        public static ISessionFactory GetSession()
        {
            return sessionFactory;
        }
    }
}