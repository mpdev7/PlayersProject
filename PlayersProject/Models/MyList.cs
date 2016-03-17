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
            Players = new List<Player>();
        }

        public virtual void AddPlayer(Player player)
        {
            player.Lists.Add(this);
            Players.Add(player);
        }

        public virtual List<Player> GetPlayer()
        {
            return Players.ToList();
        }
    }
}