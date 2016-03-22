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
    }
}