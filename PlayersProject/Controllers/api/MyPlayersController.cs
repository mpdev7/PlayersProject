using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayersProject.Models;
using NHibernate;
using NHibernate.Linq;
using Autofac;

namespace PlayersProject.Controllers.api
{
    public class MyPlayersController : ApiController
    {
        private IGetList _listget;
        private IPostPlayer _postplayer;

        public MyPlayersController(IGetList listget, IPostPlayer postplayer)
        {
            _listget = listget;
            _postplayer = postplayer;
        }                     
        

        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetMyPlayers()
        {
                return _listget.GetMyList().AsQueryable();           
        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(int id)
        { 
            if (_postplayer.PostToList(id))
            {
                return this.Ok();
            }
            else return this.Conflict();
        }

        [HttpPut]       
        public IHttpActionResult Put(int id)
        {
            _postplayer.RemoveFromList(id);

            return this.Ok();
        }
    }
}
