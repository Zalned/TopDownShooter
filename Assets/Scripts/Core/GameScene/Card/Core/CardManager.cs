using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class CardManager : NetworkBehaviour {
    [SerializeField] private CardMenuView _cardPickView;
    [Space]
    [SerializeField] private List<GameObject> _registredCards = new();

    private SessionPlayerManager _sessionPlayerManager;

    private int _maxCardsToChoose;
    private CardListGenerator _cardListGenerator = new();

    public void Initialize( SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;

        var sessionConfig = Resources.Load<SessionConfigSO>( Defines.ConfigPaths.SESSION_CONFIG );
        _maxCardsToChoose = sessionConfig.CountCardToChoose;

        if ( IsServer ) {
            EventBus.Subscribe<PlayerCardPickEvent>( HandlePlayerChoseCardFromServer );
            DebugEvents.OnShowCardPickViewBtn += StartCardChooseFromServer; // DEBUG
        }
    }

    public override void OnDestroy() {
        if ( IsServer ) {
            EventBus.Unsubscribe<PlayerCardPickEvent>( HandlePlayerChoseCardFromServer );
            DebugEvents.OnShowCardPickViewBtn -= StartCardChooseFromServer; // DEBUG
        }
    }

    public void StartCardChooseFromServer() {
        HandleStartCardChooseClientRpc();
        HandleStartCardChooseServerRpc();
    }

    [ClientRpc]
    private void HandleStartCardChooseClientRpc() {
        _cardPickView.Show();
    }

    [ServerRpc]
    private void HandleStartCardChooseServerRpc() {
        var activePlayersIDs = _sessionPlayerManager.ActivePlayers.Keys;
        var losePlayersIDs = _sessionPlayerManager.LosePlayers.Keys;

        foreach ( var activePlayerID in activePlayersIDs ) {
            if ( _sessionPlayerManager.LosePlayers.Keys.Contains( activePlayerID ) ) {

                var cardIDs = _cardListGenerator.Generate( _maxCardsToChoose, _registredCards.Count );
                SendCardsIDsTargetClientRpc( activePlayerID, cardIDs );
                HandleLosePlayersStartChooseCardsTargetClientRpc( activePlayerID );

            } else {
                // MyNote: Выполняем ту же логику что для игрока выбравшего карточку
                HandlePlayerChoseCardTargetClientRpc( activePlayerID );
            }
        }
    }

    [ClientRpc]
    private void SendCardsIDsTargetClientRpc( ulong playerID, int[] cardSoIDs ) {
        if ( NetcodeHelper.LocalClientId == playerID ) {
            _cardPickView.InstallCards( cardSoIDs );
        }
    }

    [ClientRpc]
    private void HandleLosePlayersStartChooseCardsTargetClientRpc( ulong playerID ) {
        if ( NetcodeHelper.LocalClientId == playerID ) {
            _cardPickView.ShowPickMenu();
        }
    }

    private void HandlePlayerChoseCardFromServer( PlayerCardPickEvent evt ) {
        _sessionPlayerManager.AddPlayerWhoChoseCard( evt.playerID );

        if ( _sessionPlayerManager.IsAllLosePlayersChoseCard ) {
            HandleAllPlayersChoseCardClientRpc();
        } else {
            HandlePlayerChoseCardTargetClientRpc( evt.playerID );
        }
    }

    [ClientRpc]
    private void HandleAllPlayersChoseCardClientRpc() {
        _cardPickView.Hide();
        _cardPickView.HideWaitingMenu();
        _cardPickView.HidePickMenu();
        _cardPickView.ClearCards();
    }

    [ClientRpc]
    private void HandlePlayerChoseCardTargetClientRpc( ulong playerID ) {
        if ( NetcodeHelper.LocalClientId == playerID ) {
            _cardPickView.HidePickMenu();
            _cardPickView.ShowWaitingMenu();
        }
    }

    public Card GetCardByID( int id ) {
        return _registredCards[ id ].GetComponent<Card>();
    }

    public List<Card> GetCardsByIDs( int[] ids ) {
        List<Card> cards = new();
        foreach ( var id in ids ) {
            cards.Add( GetCardByID( id ) );
        }
        return cards;
    }

    public CardSO GetCardSoById( int id ) {
        return _registredCards[ id ].GetComponent<Card>().CardSO;
    }

    public List<CardSO> GetCardSOsByIds( int[] ids ) {
        if ( ids == null ) return new List<CardSO>();
        List<CardSO> cardSOs = new();
        foreach ( var id in ids ) {
            cardSOs.Add( GetCardSoById( id ) );
        }
        return cardSOs;
    }
}