using Zenject;

public class EndGameHandler {
    private readonly MapService _mapService;
    private readonly LobbyPresenter _lobbyPresenter;
    private readonly PlayerSpawnService _playerSpawnService;
    private readonly SessionPlayerManager _sessionPlayerManager;

    public EndGameHandler(
        MapService mapService,
        LobbyPresenter lobbyPresenter,
        PlayerSpawnService playerSpawnService,
        SessionPlayerManager sessionPlayerManager ) {
        _mapService = mapService;
        _lobbyPresenter = lobbyPresenter;
        _playerSpawnService = playerSpawnService;
        _sessionPlayerManager = sessionPlayerManager;
    }

    public void HandleEndGame() {
        _mapService.DespawnMap();
        _lobbyPresenter.Show();
        _playerSpawnService.DespawnPlayersOnMap();

        _sessionPlayerManager.ResetSessionPlayers();
        _sessionPlayerManager.ResetActivePlayers();
    }
}