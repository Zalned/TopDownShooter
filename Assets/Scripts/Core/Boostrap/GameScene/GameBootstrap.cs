using Zenject;
using UnityEngine;

public class GameBootstrap : IInitializable, ITickable {
    public void Initialize() {
        if ( NetcodeHelper.IsServer ) {
            var sceneContext = Object.FindFirstObjectByType<SceneContext>();
            var container = sceneContext.Container;

            var playerManager = container.Resolve<NetworkPlayerManager>();
            var lobbyPresenter = container.Resolve<LobbyPresenter>();
            var cardManager = container.Resolve<CardManager>();
            var sessionPlayerManager = container.Resolve<SessionPlayerManager>();

            lobbyPresenter.Initialize( playerManager );
            cardManager.Initialize( sessionPlayerManager );
        }
    }
}
