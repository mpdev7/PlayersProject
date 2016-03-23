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
        IList<Player> GetMyList(int id);
        IList<MyList> GetLists();
    }

    public class GetList : IGetList
    {
        private UnitOfWork u;

        public GetList(UnitOfWork uow)
        {
            u = uow;
        }

        //Get List of players
        public virtual IList<T> Get<T>()
        {
            return u.session().Query<T>().ToList();        
        }

        //Get My List of players
        public virtual IList<Player> GetMyList(int id)
        {
            return u.session().Load<MyList>(id).Players.ToList();
        }

        //Get lists
        public virtual IList<MyList> GetLists()
        {
            return u.session().Query<MyList>().ToList();
        }
    }
}