using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller {
    [SerializeField] GameFlowController_Server _gameFlowController;
    [SerializeField] GameNetworkHandler_Server _gameNetworkHandler;

    [SerializeField] LobbyController_Server _lobbyController;
    [SerializeField] MapService_Server _mapService;

    [SerializeField] PlayerWinRoundView _playerWinRoundView;
    [SerializeField] PlayerWinGameView _playerWinGameView;

    public override void InstallBindings() {
        Container.Bind<ITickService>().To<TickService>().AsSingle().NonLazy();
        Container.Bind<BulletManager_Server>().AsSingle().NonLazy();

        if ( NetcodeHelper.IsServer ) {
            Container.Bind<GameFlowController_Server>().FromInstance( _gameFlowController ).AsSingle().NonLazy();
            Container.Bind<GameNetworkHandler_Server>().FromInstance( _gameNetworkHandler ).AsSingle().NonLazy();
            Container.Bind<GameSessionService_Server>().AsSingle().NonLazy();

            Container.Bind<PlayerFactory_Server>().AsSingle().NonLazy();
            Container.Bind<PlayerManager_Server>().AsSingle().NonLazy();

            Container.Bind<RoundController_Server>().AsSingle().NonLazy();
            Container.Bind<RoundHudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWinRoundView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWinGameView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<LobbyController_Server>().FromInstance( _lobbyController ).AsSingle().NonLazy();
            Container.Bind<MapService_Server>().FromInstance( _mapService ).AsSingle();

            Container.Bind<SessionPlayerManager_Server>().AsSingle();
            Container.Bind<PlayerSpawnController_Server>().AsSingle();
            Container.Bind<PlayerDieHandler_Server>().AsSingle().NonLazy();
        }

        Container.BindInterfacesAndSelfTo<GameBootstrap_Zenject>().AsSingle();
    }
}