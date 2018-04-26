using AvalonServer.Entities;
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
        public string Post(string gameAccessLevel, string gameName)
        {

            GameSettings.GameAccessLevel _gameAccessLevel;

            try
            {
                _gameAccessLevel = (GameSettings.GameAccessLevel) Enum.Parse(typeof(GameSettings.GameAccessLevel), gameAccessLevel, true);

            } catch(Exception ex)
            {

                return "Invalid game access type! Please use 'Public' or 'FriendsOnly'";
            }

            var request = new CreateGameMessages.Request()
            {
                accessLevel = _gameAccessLevel,
                name = gameName
            };
            var response = interactor.Handle(request);

            var jsonResponse = JsonConvert.SerializeObject(response);

            return jsonResponse;
        }
    }
}
