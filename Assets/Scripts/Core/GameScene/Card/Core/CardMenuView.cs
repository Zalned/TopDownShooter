using UnityEngine;
using UnityEngine.UI;

public class CardMenuView : MonoBehaviour {
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private CardPickHandler _cardPickController;
    [Space]
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _pickMenu;
    [SerializeField] private GameObject _waitingMenu;
    [SerializeField] private HorizontalLayoutGroup _horizontalLayout;

    private static GameObject _cardPrefab;

    private void Awake() {
        _cardPrefab = Resources.Load<GameObject>( Defines.PrefabPaths.CARD );
    }

    public void InstallRandomCards( int[] cardIDs ) {
        for ( int i = 0; i < cardIDs.Length; i++ ) {
            var cardId = cardIDs[ i ];
            var cardData = _cardManager.GetRegistredCardByID( cardId );

            var cardObj = Instantiate( _cardPrefab, _horizontalLayout.transform );

            var cardComponent = cardObj.GetComponent<Card>();
            cardComponent.Init( cardData, _cardPickController, cardId );
        }
    }

    public void ClearCards() {
        foreach ( Transform child in _horizontalLayout.transform ) {
            Destroy( child.gameObject );
        }
    }

    public void Show() { _menu.SetActive( true ); }
    public void Hide() { _menu.SetActive( false ); }

    public void ShowPickMenu() { _pickMenu.SetActive( true ); }
    public void HidePickMenu() { _pickMenu.SetActive( false ); }

    public void ShowWaitingMenu() { _waitingMenu.SetActive( true ); }
    public void HideWaitingMenu() { _waitingMenu.SetActive( false ); }
}