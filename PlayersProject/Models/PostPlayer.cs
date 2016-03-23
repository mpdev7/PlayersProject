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
        bool PostToList(int idP, int idL);
        void RemoveFromList(int idPlayer, int idList);
    }

    public class PostPlayer : IPostPlayer
    {
        private UnitOfWork u;

        public PostPlayer(UnitOfWork uow)
        {
            u = uow;
        }

        //Post new player 
        public virtual bool Post(Player p)
        {
            if ((u.session().Query<Player>().Where(x => x.Name == p.Name && x.Surname == p.Surname).Count() < 1)) {
                u.session().SaveOrUpdate(p);

                u.Commit();

                return true;
            }
            else return false;
        }

        //Add player in my list
        public virtual bool PostToList(int idPlayer, int idList)
        {
            var list = GetMyList(idList);
            if (!(exist(list, idPlayer)))
            {
                var player = GetPlayer(idPlayer);
                player.AddToMyPlayer(list);

                u.session().SaveOrUpdate(player);
                u.session().Flush();
                u.Commit();

                return true;
            }
            else return false;
        }

        //Remove player from my list
        public virtual void RemoveFromList(int idPlayer, int idList)
        {
            var player = GetPlayer(idPlayer);
            var list = GetMyList(idList);
            player.RemoveMyPlayer(list);
            u.session().SaveOrUpdate(player);
            u.Commit();
        }

        //Get list with id
        private MyList GetMyList(int id)
        {
            return u.session().Load<MyList>(id);
        }

        //Get player with id
        private Player GetPlayer(int id)
        {
            return u.session().Load<Player>(id);
        }

        private bool exist(MyList l, int id)
        {
            if (l.Players.Where(x => x.Id == id).Count() > 0)
            {
                return true;
            }
            else return false;
        }        
    }


}