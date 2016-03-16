using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public class Player
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Position { get; set; }
        public virtual string Team { get; set; }

        public virtual IList<MyList> Lists { get; set; }

        public Player()
        {
            Lists = new List<MyList>();
        }            
    }
}