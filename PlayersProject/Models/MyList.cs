using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public class MyList
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual IList<Player> Players { get; protected set; }

        public MyList()
        {
            if (Players == null)
            {
                Players = new List<Player>();
            }
        }

        public virtual void AddPlayer(Player player)
        {
            //player.Lists = this;
            //Players.Add(player);
        }

        public virtual void AddtoMyList(int i)
        {
            
        }
        
        public virtual List<Player> GetPlayer()
        {
            return Players.ToList();
        }

        public virtual bool Contain(Player p)
        {
            foreach (var item in Players)
            {
                if(item.Name == p.Name && item.Surname == p.Surname)
                {
                    return true;
                }
            }
            return false;
        }
    }
}