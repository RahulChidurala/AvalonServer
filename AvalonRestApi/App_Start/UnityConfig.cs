using AvalonServer.Gameplay.CreateGame;
using AvalonServer.Gameplay.CreatePlayer;
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
            container.RegisterType<IPlayerGateway, PlayerGatewayInMemory>();
            container.RegisterType<ICreatePlayerValidator, CreatePlayerValidator>();
            container.RegisterType<ICreatePlayerInteractor, CreatePlayerInteractor>();

            // CreateGame types
            container.RegisterType<IGameGateway, GameGatewayInMemory>();
            container.RegisterType<ICreateGameValidator, CreateGameValidator>();
            container.RegisterType<ICreateGameInteractor, CreateGameInteractor>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}