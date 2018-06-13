using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvalonServer.CreateGame
{
    public interface ICreateGameValidator
    {
        /// <summary>
        /// <exception>Throws CreateGameValidator</exception>
        /// </summary>
        void Validate(CreateGameMessages.Request request);
    }

    public class CreateGameValidator : ICreateGameValidator
    {
        public void Validate(CreateGameMessages.Request request)
        {
            var exceptionMessage = "";
            var session = request.Session;
            if (session == null || String.IsNullOrWhiteSpace(session.Id))
            {
                exceptionMessage = "Session is empty. ";
            }

            var gameName = request.GameName;
            if(String.IsNullOrWhiteSpace(gameName))
            {
                exceptionMessage += "Game name cannot be empty. ";
            }

            if(request.AccessLevel == null)
            {
                exceptionMessage += "Game must have an access level.";
            }
            
            if(String.IsNullOrWhiteSpace(exceptionMessage) == false)
            {
                throw new Exception(exceptionMessage);
            }
        }
    }
}