using UnityEngine;
using UnityEngine.UI;

public class CardMenuView : MonoBehaviour {
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private CardPickController _cardPickController;
    [Space]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _pickMenu;
    [SerializeField] private GameObject _waitingMenu;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayout;

    public void InstallCards( int[] cardIDs ) {
        for ( int i = 0; i < cardIDs.Length; i++ ) {
            var cardID = cardIDs[ i ];
            var cardPrefab = _cardManager.GetCardByID( cardID ).gameObject;
            var card = Instantiate( cardPrefab, _horizontalLayout.transform );
            var cardComponent = card.GetComponent<Card>();
            cardComponent.Init( _cardPickController );
            cardComponent.SetCardId( (ushort)cardID );
        }
    }

    public void ClearCards() {
        foreach ( Transform child in _horizontalLayout.transform ) {
            Destroy( child.gameObject );
        }
    }

    public void Show() {
        _menu.SetActive( true );
    }
    public void Hide() {
        _menu.SetActive( false );
    }

    public void ShowPickMenu() {
        _pickMenu.SetActive( true );
    }
    public void HidePickMenu() {
        _pickMenu.SetActive( false );
    }

    public void ShowWaitingMenu() {
        _waitingMenu.SetActive( true );
    }
    public void HideWaitingMenu() {
        _waitingMenu.SetActive( false );
    }
}