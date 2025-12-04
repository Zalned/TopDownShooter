using System;
using System.Threading.Tasks;

public class GameWinHandler {
    private readonly PlayerWinGameView _playerWinGameView;
    private const int SHOW_WIN_GAME_UI_TIME = 4 * 1000;

    public event Action<ulong> EndGameEvent;

    public GameWinHandler( PlayerWinGameView playerWinGameView ) {
        _playerWinGameView = playerWinGameView;
    }

    public async void HandlePlayerWinGame( ulong playerId, string name ) {
        _playerWinGameView.SetPlayerWinNameClientRpc( name );
        await ShowPlayerWinGameUI();
        EndGameEvent.Invoke( playerId );
    }

    private async Task ShowPlayerWinGameUI() {
        _playerWinGameView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_GAME_UI_TIME );
        _playerWinGameView.HideClientRpc();
    }
}