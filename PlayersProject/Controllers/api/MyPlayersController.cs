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
            using (var session = new UnitOfWork(sessionFactory))
            {
                var mylist = session.session().Query<MyList>().Count();
                if (mylist < 1)
                {
                    var addlist = new MyList { Name = "mylist" };
                    session.session().SaveOrUpdate(addlist);

                    session.Commit();
                }
                var list = session.session().Query<MyList>().Where(x => x.Name == "mylist").Single<MyList>();

                return list.Players.ToArray().AsQueryable();
            }
        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(int id)
        {            
            using (var session = new UnitOfWork(sessionFactory))
            {
                var p = session.session().Query<Player>().Where(x => x.Id == id).Single();
                var indexs = session.session().Query<MyList>();
                var index = session.session().Query<MyList>().Where(x => x.Name == "mylist").Single();

                var contain = index.Contain(p);

                if (!contain)
                {
                    p.AddToMyPlayer(index);
                    session.session().SaveOrUpdate(p);
                    session.Commit();
                    return this.Ok();
                }
                else {
                    return this.Conflict();
                }
            }
        }
        
        [HttpPut]       
        public IHttpActionResult Put(int id)
        {            
            using (var session = new UnitOfWork(sessionFactory))
            {
                var p = session.session().Query<Player>().Where(x => x.Id == id).Single();

                p.RemoveMyPlayer();
                session.session().SaveOrUpdate(p);
                session.Commit();

                return this.Ok();
            }
        }
    }
}
