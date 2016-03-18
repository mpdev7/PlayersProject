using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public class UnitOfWork : IDisposable 
    {
        private ISession Session;
        private ITransaction transaction;

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            Session = sessionFactory.OpenSession();
            transaction = Session.BeginTransaction();
        }

        public ISession session()
        {
            return Session;
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            Session.Close();
        }          
        

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }
    }
}