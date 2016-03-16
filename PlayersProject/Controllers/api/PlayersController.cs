using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayersProject.Models;
using NHibernate;
using NHibernate.Linq;

namespace PlayersProject.Controllers.api
{
    public class PlayersController : ApiController
    {
        private static List<Player> PlayersList = new List<Player>();

        private static ISessionFactory sessionFactory;

        public PlayersController()
        {
            sessionFactory = NHibernateHelper.CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    MyList list = new MyList { Name = "list" };
                    Player cataldi = new Player { Name = "Danilo", Surname = "Cataldi", Position = "Midfielder", Team = "Lazio" };
                    //list.AddPlayer(cataldi);

                    PlayersList.Add(cataldi);
                    
                    session.SaveOrUpdate(cataldi);

                    transaction.Commit();
                }
            }
        }

        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            using (var session =  sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var mylist = session.Query<MyList>().Where(x => x.Id == 1).Single<MyList>();

                    //var myplayer = from x in session.Query<Player>()
                    //               select new
                    //               {
                    //                   x.Id,                                       
                    //                   x.Name,
                    //                   x.Position,
                    //                   x.Surname,
                    //                   x.Team
                    //               };
                    //var get = myplayer.ToArray();
                          
                    //return get.AsQueryable();
                }
            }
            return PlayersList.ToArray().AsQueryable();
        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(Player player)
        {
            var index = PlayersList.FindIndex(p => p.Name == player.Name && p.Surname == player.Surname);

            if (index != -1)
            {
                return this.Conflict();
            }
            else {

                player.Id = PlayersList.Count();

                if (this.ModelState.IsValid)
                {
                    PlayersList.Add(player);
                    return this.Ok();                    
                }
                else
                {
                    return this.BadRequest();
                }
                
            }
        }
    }
}
