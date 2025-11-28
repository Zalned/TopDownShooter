using Zenject;
using UnityEngine;

public class GameBootstrap : IInitializable, ITickable {
    public void Initialize() {
        if ( NetcodeHelper.IsServer ) {
            var sceneContext = Object.FindFirstObjectByType<SceneContext>();
            var container = sceneContext.Container;

            var gameSessionService = container.Resolve<GameSessionService>();
            var sceneLoader = container.Resolve<SceneLoader>();
            var playerManager = container.Resolve<PlayerManager>();

            var gameFlowController = container.Resolve<GameFlowController>();
            var gameNetworkHandler = container.Resolve<GameNetworkHandler>();
            var lobbyController = container.Resolve<LobbyPresenter>();

            gameFlowController.Initialize( gameSessionService );
            gameNetworkHandler.Initialize( sceneLoader );
            lobbyController.Initialize( playerManager );
        } 
    }
}
