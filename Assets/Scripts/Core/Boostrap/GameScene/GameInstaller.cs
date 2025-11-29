using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller {
    [SerializeField] private GameFlowController _gameFlowController;
    [SerializeField] private GameNetworkHandler _gameNetworkHandler;
    [SerializeField] private MapService_Server _mapService;

    [SerializeField] private LobbyPresenter _lobbyPresenter;
    [SerializeField] private PlayerWinRoundView _playerWinRoundView;
    [SerializeField] private PlayerWinGameView _playerWinGameView;

    public override void InstallBindings() {
        Container.Bind<ITickService>().To<TickService>().AsSingle().NonLazy();
        Container.Bind<BulletManager>().AsSingle().NonLazy();

        if ( NetcodeHelper.IsServer ) {
            Container.Bind<GameFlowController>().FromInstance( _gameFlowController ).AsSingle().NonLazy();
            Container.Bind<GameNetworkHandler>().FromInstance( _gameNetworkHandler ).AsSingle().NonLazy();
            Container.Bind<GameSessionService>().AsSingle().NonLazy();

            Container.Bind<MapService_Server>().FromInstance( _mapService ).AsSingle();
            Container.Bind<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<PlayerManager>().AsSingle().NonLazy();

            Container.Bind<RoundManager>().AsSingle().NonLazy();
            Container.Bind<RoundHudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWinRoundView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWinGameView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LobbyPresenter>().FromInstance( _lobbyPresenter ).AsSingle().NonLazy();

            Container.Bind<SessionPlayerManager>().AsSingle();
            Container.Bind<PlayerSpawnService>().AsSingle();
            Container.Bind<PlayerDieHandler>().AsSingle().NonLazy();
        }

        Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle();
    }
}