using AvalonDatalayer;
using AvalonServer.CreateGame;
using AvalonServer.Gameplay.CreatePlayer;
using AvalonServer.Gameplay.JoinGame;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace AvalonRestApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
           
            // CreatePlayer types
            container.RegisterType<IPlayerGateway, PlayerDatabase>();
            container.RegisterType<ICreatePlayerValidator, CreatePlayerValidator>();
            container.RegisterType<ICreatePlayerInteractor, CreatePlayerInteractor>();

            // CreateGame types
            container.RegisterType<IGameGateway, GameDatabase>();
            container.RegisterType<ICreateGameValidator, CreateGameValidator>();
            container.RegisterType<ICreateGameInteractor, CreateGameInteractor>();

            // JoinGame types
            container.RegisterType<IJoinGameValidator, JoinGameValidator>();
            container.RegisterType<IJoinGameInteractor, JoinGameInteractor>();            

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}