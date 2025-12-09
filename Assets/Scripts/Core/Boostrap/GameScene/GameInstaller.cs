using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller {
    [SerializeField] private MapService _mapService;

    [SerializeField] private LobbyPresenter _lobbyPresenter;
    [SerializeField] private PlayerWinRoundView _playerWinRoundView;
    [SerializeField] private PlayerWinGameView _playerWinGameView;

    [SerializeField] private CardManager _cardManager;
    [SerializeField] private CardPickHandler _cardPickHandler;

    public override void InstallBindings() {
        Container.Bind<ITickService>().To<TickService>().AsSingle().NonLazy();
        Container.Bind<BulletManager>().AsSingle().NonLazy();
        Container.Bind<CardManager>().FromInstance( _cardManager ).AsSingle().NonLazy();

        if ( NetcodeHelper.IsServer ) {
            Container.Bind<GameFlowController>().AsSingle().NonLazy();

            Container.Bind<MapService>().FromInstance( _mapService ).AsSingle();
            Container.Bind<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<NetworkPlayerManager>().AsSingle().NonLazy();

            Container.Bind<RoundManager>().AsSingle().NonLazy();
            Container.Bind<RoundHudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWinRoundView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerWinGameView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<LobbyPresenter>().FromInstance( _lobbyPresenter ).AsSingle().NonLazy();

            Container.Bind<SessionPlayerManager>().AsSingle();
            Container.Bind<PlayerSpawnService>().AsSingle();
            Container.Bind<PlayerDieHandler>().AsSingle().NonLazy();

            Container.Bind<CardPickHandler>().FromInstance( _cardPickHandler ).AsSingle().NonLazy();
            Container.Bind<CardContext>().AsSingle();
            Container.Bind<ExplosionService>().AsSingle();
        }

        Container.BindInterfacesAndSelfTo<GameBootstrap>().AsSingle();
    }
}