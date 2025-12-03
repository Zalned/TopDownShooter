using System;
using Zenject;

public class CardPickService : IDisposable {
    private SessionPlayerManager _sessionPlayerManager;

    [Inject]
    public CardPickService( SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;

        EventBus.Subscribe<PlayerCardPickEvent>( OnPlayerPickCard );
    }
    public void Dispose() {
        EventBus.Unsubscribe<PlayerCardPickEvent>( OnPlayerPickCard );
    }

    private void OnPlayerPickCard( PlayerCardPickEvent evt ) {
        var activePlayer = _sessionPlayerManager.GetActivePlayerByID( evt.playerID );
        activePlayer.CardDeck.Add( evt.CardSO );
    }
}