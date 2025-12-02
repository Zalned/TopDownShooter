using UnityEngine;
using UnityEngine.UI;

public class CardPickView : MonoBehaviour {
    [SerializeField] private CardManager _cardManager;
    [Space]
    [SerializeField] private GameObject _menu;
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

    public void Show() {
        _menu.gameObject.SetActive( true );
    }
    public void Hide() {
        _menu.gameObject.SetActive( false );
    }
}