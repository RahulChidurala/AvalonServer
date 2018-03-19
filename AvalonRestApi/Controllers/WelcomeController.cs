using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AvalonRestApi.Controllers
{
    public class WelcomeController : ApiController
    {
        // GET: api/Default
        public string Get()
        {
            return "Welcome to the Avalon Web API!";
        }
    }
}
