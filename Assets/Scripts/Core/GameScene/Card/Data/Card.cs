using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    [SerializeField] private CardSO _cardSO;
    [Space]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space]
    [SerializeField] private Button _pickUpBtn;

    private void Awake() {
        _nameText.text = _cardSO.Name;
        _descriptionText.text = _cardSO.Description;

        _pickUpBtn.onClick.AddListener( OnCardPicked );
    }

    private int _cardID;
    private ulong _playerID = 0;
    public void SetPlayerID( ulong playerID ) {
        _playerID = playerID;
    }
    public void SetCardID( int cardID ) {
        _cardID = cardID;
    }

    private void OnCardPicked() {
        EventBus.Publish( new OnPlayerPickCard( _playerID, _cardSO ) ); // MyTodo: Replace 0 with actual player ID
    }
}