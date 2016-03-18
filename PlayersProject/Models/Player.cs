using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public class Player
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Position { get; set; }
        public virtual string Team { get; set; }

        public virtual MyList Lists { get; set; }


        public virtual void AddToMyPlayer(MyList l)
        {
            Lists = l;
            l.Players.Add(this);
        }

        public virtual void RemoveMyPlayer()
        {
            Lists.Players.Remove(this);
            Lists = null;
        }
    }
}