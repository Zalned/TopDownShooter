using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private LayoutGroup _statsLayotGroup;
    [SerializeField] private Button _btn;
    [Space]
    [SerializeField] private TextMeshProUGUI _cardStatTextExample;

    private int _cardId = -1;

    private CardData _cardData;
    public CardData CardData => _cardData;

    private CardPickHandler _controller;
    private CardStatsTextInitializer _statsTextInitializer;

    public void SetCardData( CardData cardData ) {
        _cardData = cardData;
    }

    public void Init( CardData cardData, CardPickHandler controller, int cardId ) {
        _cardData = cardData;
        _controller = controller;
        _cardId = cardId;
        _statsTextInitializer = new();

        _nameText.text = _cardData.Title;
        _descriptionText.text = _cardData.Description;

        _statsTextInitializer.InitializeCardStatTexts(
            _cardData.CardStats, _statsLayotGroup.transform, _cardStatTextExample );

        _btn.onClick.AddListener( OnPicked );
    }

    private void OnDestroy() {
        _btn.onClick.RemoveListener( OnPicked );
    }

    private void OnPicked() {
        var playerId = NetcodeHelper.LocalClientId;
        _controller.HandleCardPickedToServerRpc( playerId, _cardId ); // Стоит обрабатывать через событие
    }
}