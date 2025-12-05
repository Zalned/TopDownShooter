public class StartGameHandler {
    private readonly NetworkPlayerManager _playerManager;
    private readonly SessionPlayerManager _sessionPlayerManager;
    private readonly LobbyPresenter _lobbyPresenter;

    public StartGameHandler(
        NetworkPlayerManager networkPlayerManager,
        SessionPlayerManager sessionPlayerManager,
        LobbyPresenter lobbyPresenter) {
        _playerManager = networkPlayerManager;
        _sessionPlayerManager = sessionPlayerManager;
        _lobbyPresenter = lobbyPresenter;
    }

    public void HandleStartGame() {
        _sessionPlayerManager.SetSessionPlayers( _playerManager.RegistredPlayers );
        _sessionPlayerManager.SetActivePlayers( _sessionPlayerManager.SessionPlayers );

        _lobbyPresenter.Hide();
    }
}