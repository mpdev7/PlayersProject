using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models.Mappings
{
    public class PlayerMap : ClassMap<Player> 
    {
        public PlayerMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Surname);
            Map(x => x.Position);
            Map(x => x.Team);
            References(x => x.Lists)
                .Cascade.All();
        }
    }
}