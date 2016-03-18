using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayersProject.Models;
using NHibernate;
using NHibernate.Linq;
using Newtonsoft.Json;

namespace PlayersProject.Controllers.api
{
    public class PlayersController : ApiController
    {
        private static ISessionFactory sessionFactory;

        public PlayersController()
        {
            if (sessionFactory == null)
            {
                sessionFactory =  NHibernateHelper.GetSession();
            }
        }

        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            using (var session = new UnitOfWork(sessionFactory))
            {
                var PlayerList = session.session().Query<Player>();
                PlayerList = PlayerList.ToArray().AsQueryable();

                return PlayerList;
            }

            //using (var session = sessionFactory.OpenSession())
            //{
            //    using (var transaction = session.BeginTransaction())
            //    {
            //        var PlayerList = session.Query<Player>();
            //        PlayerList = PlayerList.ToArray().AsQueryable();

            //        return PlayerList;
            //    }
            //}

        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(Player player)
        {
            using(var session = new UnitOfWork(sessionFactory))
            {
                var index = session.session().Query<Player>().Count(x => x.Name == player.Name && x.Surname == player.Surname);

                if (index > 0)
                {
                    return this.Conflict();
                }
                else {
                    if (this.ModelState.IsValid)
                    {
                        session.session().SaveOrUpdate(player);
                        session.Commit();

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
}
