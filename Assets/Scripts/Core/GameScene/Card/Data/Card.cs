using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {
    [SerializeField] private CardSO _cardSO;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _cardStatText;
    [SerializeField] private Button _btn;

    public CardSO CardSO => _cardSO;
    public ushort CardId { get; private set; }
    public int PlayerId { get; private set; }

    private CardPickController _controller;

    public void Init( CardPickController controller ) {
        _controller = controller;

        _nameText.text = _cardSO.Name;
        _descriptionText.text = _cardSO.Description;
        _cardStatText.text = _cardSO.StatDescription;
        _btn.onClick.AddListener( OnPicked );
    }

    public void SetCardId( ushort cardId ) {
        CardId = cardId;
    }

    private void OnDestroy() {
        _btn.onClick.RemoveListener( OnPicked );
    }

    private void OnPicked() {
        _controller.OnCardPicked( NetcodeHelper.LocalClientId, CardId );
    }
}
