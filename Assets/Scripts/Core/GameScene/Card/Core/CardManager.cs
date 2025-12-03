using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Refactor: в классе смешена сетевая и локальная логика
public class CardManager : NetworkBehaviour {
    [SerializeField] private CardPickView _cardPickView;
    [Space]
    [SerializeField] private List<GameObject> _cards = new();

    private SessionPlayerManager _sessionPlayerManager;

    private int _maxCardsToChoose;
    private CardListGenerator _cardListGenerator = new();

    public void Initialize( SessionPlayerManager sessionPlayerManager ) {
        _sessionPlayerManager = sessionPlayerManager;

        var sessionConfig = Resources.Load<SessionConfigSO>( Defines.ConfigPaths.SESSION_CONFIG );
        _maxCardsToChoose = sessionConfig.CountCardToChoose;

        if ( IsServer ) {
            EventBus.Subscribe<PlayerCardPickEvent>( OnPlayerPickCardServer );
            DebugEvents.OnShowCardPickViewBtn += StartCardChooseServer; // DEBUG
        }
    }

    public override void OnDestroy() {
        if ( IsServer ) {
            EventBus.Unsubscribe<PlayerCardPickEvent>( OnPlayerPickCardServer );
            DebugEvents.OnShowCardPickViewBtn -= StartCardChooseServer; // DEBUG
        }
    }

    public void StartCardChooseServer() {
        StartCardChooseClientRpc();
        _cardPickView.ShowClientRpc(); // Почему-то не запускается для всех клиентов
    }

    [ClientRpc]
    public void StartCardChooseClientRpc() {
        HandleCardSelectionRequestServerRpc( NetworkManager.LocalClientId );
    }

    [Rpc( SendTo.Server )]
    private void HandleCardSelectionRequestServerRpc( ulong clientId ) {
        bool isLosePlayer = _sessionPlayerManager.LosePlayers.ContainsKey( clientId );

        if ( isLosePlayer ) {
            int[] ids = _cardListGenerator.Generate( _maxCardsToChoose, _cards.Count );
            SendCardListTargetClientRpc( ids, clientId );
            ShowPickMenuTargetClientRpc( clientId );
        } else {
            ShowWaitingMenuTargetClientRpc( clientId );
        }
    }

    [ClientRpc]
    private void ShowPickMenuTargetClientRpc( ulong targetClientId ) {
        if ( NetworkManager.LocalClientId == targetClientId ) {
            _cardPickView.ShowPickMenu();
        }
    }

    [ClientRpc]
    private void ShowWaitingMenuTargetClientRpc( ulong targetClientId ) {
        if ( NetworkManager.LocalClientId == targetClientId ) {
            _cardPickView.ShowWaitingMenu();
        }
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void RequestCardListServerRpc( ulong clientId ) {
        int[] ids = _cardListGenerator.Generate( _maxCardsToChoose, _cards.Count );
        SendCardListTargetClientRpc( ids, clientId );
    }

    [ClientRpc]
    private void SendCardListTargetClientRpc( int[] ids, ulong targetClientId ) {
        if ( NetcodeHelper.LocalClientId == targetClientId ) {
            foreach ( var card in _cards ) {
                card.GetComponent<Card>().SetPlayerID( targetClientId );
            }
            _cardPickView.ShowCards( ids );
        }
    }

    private void OnPlayerPickCardServer( PlayerCardPickEvent evt ) {
        HandlePlayerChoseCardTargetClientRpc( evt.playerID );
        HandlePlayerChoseCardServer();
    }

    [ClientRpc]
    public void HandlePlayerChoseCardTargetClientRpc( ulong targetClientId ) {
        if ( NetcodeHelper.LocalClientId == targetClientId ) {
            _cardPickView.HidePickMenu();
            _cardPickView.ClearCards();
            _cardPickView.ShowWaitingMenu();
        }
    }

    public void HandlePlayerChoseCardServer() {
        if ( _sessionPlayerManager.IsAllLosePlayersChoseCard ) {
            _cardPickView.HideClientRpc();
            _cardPickView.HideWaitingMenuClientRpc();
        }
    }

    public Card GetCardByID( int id ) {
        return _cards[ id ].GetComponent<Card>();
    }

    public List<Card> GetCardsByIDs( int[] ids ) {
        List<Card> cards = new();
        foreach ( var id in ids ) {
            cards.Add( GetCardByID( id ) );
        }
        return cards;
    }

    public CardSO GetCardSoById( int id ) {
        return _cards[ id ].GetComponent<Card>().CardSO;
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