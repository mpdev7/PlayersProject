using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlayersProject.Models
{
    public interface IPostPlayer
    {
        bool Post(Player p);
        bool PostToList(int id);
        void RemoveFromList(int id);
    }

    public class PostPlayer : IPostPlayer
    {
        private UnitOfWork u;

        public PostPlayer(UnitOfWork uow)
        {
            u = uow;
        }

        public virtual bool Post(Player p)
        {
            if ((u.session().Query<Player>().Where(x => x.Name == p.Name && x.Surname == p.Surname).Count() < 1)) {
                u.session().SaveOrUpdate(p);

                u.Commit();

                return true;
            }
            else return false;
        }

        public virtual bool PostToList(int id)
        {
            var list = GetMyList();
            if (!(exist(list, id)))
            {
                GetPlayer(id).AddToMyPlayer(list);

                u.session().Save(GetPlayer(id));
                u.Commit();

                return true;
            }
            else return false;
        }

        public virtual void RemoveFromList(int id)
        {
            var player = GetPlayer(id);
            player.RemoveMyPlayer();
            u.session().SaveOrUpdate(player);
            u.Commit();
        }

        public MyList GetMyList()
        {
            return u.session().Query<MyList>().Where(x => x.Name == "mylist").Single();
        }

        private bool exist(MyList l, int id)
        {
            if (l.Players.Where(x => x.Id == id).Count() > 0)
            {
                return true;
            }
            else return false;
        }

        private Player GetPlayer(int id)
        {
            return u.session().Load<Player>(id);
        }
    }


}