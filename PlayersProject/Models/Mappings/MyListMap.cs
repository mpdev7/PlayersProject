using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlayersProject.Models.Mappings
{
    public class MyListMap : ClassMap<MyList>
    {
        public MyListMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany(x => x.Players)
                .Cascade.All()
                .Inverse();                                                                     
        }
    }
}