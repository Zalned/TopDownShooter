using System;
using Zenject;

public class CardPickService : IDisposable {
    private SessionPlayerManager _sessionPlayerManager;
    private CardManager _cardManager;

    [Inject]
    public CardPickService(
        SessionPlayerManager sessionPlayerManager,
        CardManager cardManager ) {

        _sessionPlayerManager = sessionPlayerManager;
        _cardManager = cardManager;

        EventBus.Subscribe<PlayerCardPickEvent>( OnPlayerPickCard );
    }
    public void Dispose() {
        EventBus.Unsubscribe<PlayerCardPickEvent>( OnPlayerPickCard );
    }

    private void OnPlayerPickCard( PlayerCardPickEvent evt ) {
        var activePlayer = _sessionPlayerManager.GetActivePlayerByID( evt.playerID );
        activePlayer.CardDeck.Add( _cardManager.GetCardSoById( evt.CardId ) );
    }
}