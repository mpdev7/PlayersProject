using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlayersProject.Models
{
    public interface IPostPlayer
    {
        void Post(Player p);
    }

    public class PostPlayer
    {
        private UnitOfWork u;

        public PostPlayer(UnitOfWork uow)
        {
            u = uow;
        }

        public virtual Post(Player p)
        {
            u.session().SaveOrUpdate(p);

            u.Commit();
            
        }
    }
}