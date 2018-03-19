using AvalonServer.Gameplay.CreateGame;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AvalonRestApi.Controllers
{
    public class CreateGameController: ApiController
    {

        private ICreateGameInteractor interactor;

        public CreateGameController(ICreateGameInteractor interactor)
        {
            this.interactor = interactor;
        }

        [HttpPost]
        public string Post([FromBody]CreateGameMessages.Request request)
        {
            var response = interactor.Handle(request);

            var jsonResponse = JsonConvert.SerializeObject(response);

            return jsonResponse;
        }
    }
}
