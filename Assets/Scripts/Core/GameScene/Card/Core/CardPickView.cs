using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CardPickView : MonoBehaviour {
    [SerializeField] private CardManager _cardManager;
    [Space]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _pickMenu;
    [SerializeField] private GameObject _waitingMenu;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayout;

    public void ShowCards( int[] cardIDs ) {
        for ( int i = 0; i < cardIDs.Length; i++ ) {
            var cardID = cardIDs[ i ];
            var cardPrefab = _cardManager.GetCardByID( cardID ).gameObject;
            Instantiate( cardPrefab, _horizontalLayout.transform );
        }
    }

    public void ClearCards() {
        foreach ( Transform child in _horizontalLayout.transform ) {
            Destroy( child.gameObject );
        }
    }

    [ClientRpc]
    public void ShowClientRpc() {
        _menu.SetActive( true );
    }
    [ClientRpc]
    public void HideClientRpc() {
        _menu.SetActive( false );
    }

    public void ShowPickMenu() {
        _pickMenu.SetActive( true );
    }
    public void HidePickMenu() {
        _pickMenu.SetActive( false );
    }


    [ClientRpc]
    public void HideWaitingMenuClientRpc() {
        _waitingMenu.SetActive( false );
    }
    public void ShowWaitingMenu() {
        _waitingMenu.SetActive( true );
    }
    public void HideWaitingMenu() {
        _waitingMenu.SetActive( false );
    }
}