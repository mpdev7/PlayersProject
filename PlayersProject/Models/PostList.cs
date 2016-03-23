using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public interface IPostList
    {
        bool Post(MyList l);
    }

    public class PostList : IPostList
    {
        private UnitOfWork u;

        public PostList(UnitOfWork uow)
        {
            u = uow;
        }

        //Post new list
        public virtual bool Post(MyList l)
        {
            if (check(l))
            {
                u.session().Save(l);
                u.Commit();
                return true;
            }
            else return false;
        }

        //Check the request
        private bool check(MyList l)
        {
            if (l.Name != "" && !(exist(l)))
            {
                return true;
            }
            else return false;
        }
      
        private bool exist(MyList l)
        {
            if (u.session().Query<MyList>().Where(x => x.Name == l.Name).Count() > 0)
            {
                return true;
            }
            else return false;
        }
    }
}