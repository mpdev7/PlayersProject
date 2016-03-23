using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models
{
    public interface IPlayer
    {
        void AddToMyPlayer(MyList l);
        void RemoveMyPlayer(MyList l);
    }

    public class Player : IPlayer
    {
        private int _addCount;

        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Position { get; set; }
        public virtual string Team { get; set; }
        public virtual int AddCount {
            get {return _addCount; }
            set {_addCount = Lists.Count(); }
        }

        public virtual IList<MyList> Lists { get; set; }


        public virtual void AddToMyPlayer(MyList l)
        {
            Lists.Add(l);

            l.Players.Add(this);
        }

        public virtual void RemoveMyPlayer(MyList l)
        {            
            l.Players.Remove(this);

            var index = Lists.IndexOf(Lists.Where(x => x.Name == l.Name).Single());
            Lists[index] = null;
        }
    }
}