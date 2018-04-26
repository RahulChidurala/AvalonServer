using AvalonServer.Gameplay.CreatePlayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AvalonRestApi.Controllers
{
    public class CreatePlayerController : ApiController
    {
        private ICreatePlayerInteractor interator;

        public CreatePlayerController(ICreatePlayerInteractor interactor)
        {
            this.interator = interactor;
        }

        // POST api/createplayer
        [HttpPost]
        public string Post(string username)
        {

            var request = new CreatePlayerMessages.Request()
            {
                Username = username
            };

            var response = interator.Handle(request);
            var jsonResponse = JsonConvert.SerializeObject(response);

            return jsonResponse;
        }
    }
}
