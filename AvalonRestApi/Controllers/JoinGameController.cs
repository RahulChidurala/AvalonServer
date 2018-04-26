using AvalonServer.Gameplay.JoinGame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AvalonRestApi.Controllers
{
    public class JoinGameController : ApiController
    {

        private IJoinGameInteractor interactor;

        public JoinGameController(IJoinGameInteractor interactor)
        {
            this.interactor = interactor;
        }

        [HttpPost]        
        // POST api/<controller>
        public string Post(string playerName, int gameId)
        {
            var request = new JoinGameMessages.Request()
            {
                playerUsername = playerName,
                gameId = gameId
            };

            var response = interactor.Handle(request);

            var jsonResponse = JsonConvert.SerializeObject(response);
            return jsonResponse;
        }
    }
}