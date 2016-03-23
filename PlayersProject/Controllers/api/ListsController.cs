using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlayersProject.Models;

namespace PlayersProject.Controllers.api
{
    public class ListsController : ApiController
    {
        IGetList _getlist;
        IPostList _postlist;

        public ListsController(IGetList getList, IPostList postList)
        {
            _getlist = getList;
            _postlist = postList;
        }

        // GET: api/Lists
        [HttpGet]
        public IQueryable<MyList> Get()
        {
            return _getlist.GetLists().AsQueryable();
        }

        // POST: api/Lists
        public IHttpActionResult Post(MyList l)
        {
            if (_postlist.Post(l))
            {
                return this.Ok();
            }
            else return this.Conflict();            
        }

    }
}
