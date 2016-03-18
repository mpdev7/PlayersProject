using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public interface IGetList
    {
        IList<T> Get<T>();
        IList<Player> GetMyList();
        bool exist(Player p);
    }

    public class GetList : IGetList
    {
        private UnitOfWork u;

        public GetList(UnitOfWork uow)
        {
            u = uow;
        }

        public virtual IList<T> Get<T>()
        {
            return u.session().Query<T>().ToList();        
        }

        public virtual IList<Player> GetMyList()
        {
            var mylist = u.session().Query<MyList>().Count();
            if (mylist < 1)
            {
                var addlist = new MyList { Name = "mylist" };
                u.session().SaveOrUpdate(addlist);

                u.Commit();
            }

            return u.session().Query<MyList>().Where(x => x.Name == "mylist").Single().Players.ToList();
        }

        public virtual bool exist(Player p)
        {
            var e = Get<Player>().Where(x => x.Name == p.Name && x.Surname == p.Surname).Count();

            if (e > 0) return true;
            else return false;
        }
    }
}