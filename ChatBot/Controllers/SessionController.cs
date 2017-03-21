using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using StackExchange.Redis;

namespace TogetherChatBot.Controllers
{
    public class SessionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult SaveSession(string client)
        {

            IDatabase cache = CacheManager.Connection.GetDatabase();

            // Perform cache operations using the cache object...
            // Simple put of integral data types into the cache
            cache.StringSet("InitiatePOCFor", client);
            //cache.StringSet("key2", 25);

            return Ok();
        }

       
    }
}
