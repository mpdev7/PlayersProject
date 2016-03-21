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
using Autofac;

namespace PlayersProject.Controllers.api
{
    public class PlayersController : ApiController
    {
        private IGetList _listGet;
        private IPostPlayer _postPlayer;

        public PlayersController(IGetList listGet, IPostPlayer postPlayer)
        {
            _listGet = listGet;
            _postPlayer = postPlayer;
        }

        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            return _listGet.Get<Player>().AsQueryable(); 
        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(Player player)
        {             
            if (_postPlayer.Post(player))
            {
                return this.Ok();
            }
            else return this.Conflict();
        }
    }
}
