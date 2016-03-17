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
    public class MyPlayersController : ApiController
    {
        private ISessionFactory sessionFactory;

        private static List<Player> MyPlayersList = new List<Player>();

        public MyPlayersController()
        {
            sessionFactory = NHibernateHelper.GetSession();                       
        }

        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetMyPlayers()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var mylist = session.Query<MyList>().Count();    
                            if (mylist < 1)
                            {
                                var addlist = new MyList { Name = "mylist" };
                                session.SaveOrUpdate(addlist);
                            }                       
                    var list = session.Query<MyList>().Where(x => x.Name == "mylist").Single<MyList>();

                    transaction.Commit();
                    return list.Players.ToArray().AsQueryable();               
                }
            }
        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var p = session.Query<Player>().Where(x => x.Id == id).Single();
                    var index = session.Query<MyList>().Where(x => x.Name == "mylist").Single();

                    var contain = index.Contain(p);

                    if (!contain)
                    {
                        p.AddToMyPlayer(index);
                        session.SaveOrUpdate(p);
                        transaction.Commit();                  
                        return this.Ok();
                    }
                    else {
                        return this.Conflict();
                    } 
                }
            }
        }
        
        [HttpPut]       
        public IHttpActionResult Put(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var p = session.Query<Player>().Where(x => x.Id == id).Single();

                    p.RemoveMyPlayer();
                    session.SaveOrUpdate(p);
                    transaction.Commit();

                    return this.Ok();
                }
            }
        }
    }
}
