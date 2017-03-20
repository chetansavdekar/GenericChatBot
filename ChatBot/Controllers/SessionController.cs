using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace TogetherChatBot.Controllers
{
    public class SessionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult SaveSession(string client)
        {
            var session = HttpContext.Current.Application;
            if (session != null)
            {
               session["InitiatePOCFor"] = client;
            }
            // HttpContext.Current.Session["InitiatePOCFor"] = client;
            return Ok();
        }
    }
}
