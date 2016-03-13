using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayersProject.Models;

namespace PlayersProject.Controllers.api
{
    public class MyPlayersController : ApiController
    {
        private static List<Player> MyPlayersList = new List<Player>();

        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            var list = MyPlayersList.ToArray();

            //System.Threading.Thread.Sleep(2000);
            return list.AsQueryable();
        }

        //POST api/Players
        [HttpPost]
        public IHttpActionResult Post(Player player)
        {
            var index = MyPlayersList.FindIndex(p => p.Name == player.Name && p.Surname == player.Surname);

            if (index != -1)
            {
                return this.Conflict();
            }
            else {
                if (this.ModelState.IsValid)
                {
                    player.Id = MyPlayersList.Count();
                    MyPlayersList.Add(player);
                    return this.Ok();
                }
                else
                {
                    return this.BadRequest();
                }
            }
        }

        [HttpPut]
        public IHttpActionResult Put(Player player)
        {            
            if (!(this.ModelState.IsValid))
            {
                return this.BadRequest();
            }
            else if (this.ModelState.IsValid)
            {
                var index = MyPlayersList.FindIndex(a => a.Id == player.Id);
                MyPlayersList.RemoveAt(index);
                return this.Ok();
            }
            return this.NotFound();
        }
    }
}
