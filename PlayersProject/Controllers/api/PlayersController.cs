using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayersProject.Models;

namespace PlayersProject.Controllers.api
{
    public class PlayersController : ApiController
    {
        private static List<Player> PlayersList = new List<Player>();
        
        //GET api/Players
        [HttpGet]
        public IQueryable<Player> GetPlayers()
        {
            var list = PlayersList.ToArray();
                  
            return list.AsQueryable();
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
