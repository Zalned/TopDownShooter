using Zenject;
using UnityEngine;

public class GameBootstrap_Zenject : IInitializable, ITickable {
    public void Initialize() {
        if ( NetcodeHelper.IsServer ) {
            var sceneContext = Object.FindFirstObjectByType<SceneContext>();
            var container = sceneContext.Container;

            var gameSessionService = container.Resolve<GameSessionService_Server>();
            var sceneLoader = container.Resolve<SceneLoader>();
            var playerManager = container.Resolve<PlayerManager_Server>();

            var gameFlowController = container.Resolve<GameFlowController_Server>();
            var gameNetworkHandler = container.Resolve<GameNetworkHandler_Server>();
            var lobbyController = container.Resolve<LobbyController_Server>();

            gameFlowController.Initialize( gameSessionService );
            gameNetworkHandler.Initialize( sceneLoader );
            lobbyController.Initialize( playerManager );
        } 
    }
}
