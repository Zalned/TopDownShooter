using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {
    [SerializeField] private CardSO _cardSO;
    [Space]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space]
    [SerializeField] private Button _pickUpBtn;

    public CardSO CardSO => _cardSO;

    private void Awake() {
        _nameText.text = _cardSO.Name;
        _descriptionText.text = _cardSO.Description;

        _pickUpBtn.onClick.AddListener( OnCardPicked );
    }

    private ulong _playerID;
    public void SetPlayerID( ulong playerID ) {
        _playerID = playerID;
    }

    private void OnCardPicked() {
        NotifyCardPickedServerRpc();
    }

    [ServerRpc]
    private void NotifyCardPickedServerRpc() {
        EventBus.Publish( new PlayerCardPickEvent( _playerID, _cardSO ) );
    }
}