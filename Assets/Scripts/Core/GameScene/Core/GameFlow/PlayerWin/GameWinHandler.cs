using System;
using System.Threading.Tasks;

public class GameWinHandler {
    private readonly PlayerWinGameView _playerWinGameView;
    private const int SHOW_WIN_GAME_UI_TIME = 4 * 1000;

    public GameWinHandler( PlayerWinGameView playerWinGameView ) {
        _playerWinGameView = playerWinGameView;
    }

    public async Task HandlePlayerWinGame( ulong playerId, string name ) {
        _playerWinGameView.SetPlayerWinNameClientRpc( name );
        await ShowPlayerWinGameUI();
    }

    private async Task ShowPlayerWinGameUI() {
        _playerWinGameView.ShowClientRpc();
        await Task.Delay( SHOW_WIN_GAME_UI_TIME );
        _playerWinGameView.HideClientRpc();
    }
}