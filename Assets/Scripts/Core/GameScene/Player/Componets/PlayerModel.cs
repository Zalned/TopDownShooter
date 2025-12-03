using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerModel {
    public PlayerStats PlayerStats = new();
    public ReactiveProperty<float> CurrentHealth;

    private CardContext _ctx;
    private readonly List<IEffect> _effects = new();

    public void SetCards(List<CardSO> cards ) {
        foreach ( var card in cards ) {
            AddCard( card );
        }
    }

    private void AddCard( CardSO card ) {
        Debug.Log( $"Adding card {card.Name} to player" );

        foreach ( var effectSO in card.Effects ) {
            Debug.Log( $"Aplly effect {effectSO.name} to player" );

            var eff = effectSO.CreateRuntime();
            eff.Install( PlayerStats, _ctx );
            _effects.Add( eff );
        }
    }

    public void ClearEffects() {
        foreach ( var e in _effects )
            e.Uninstall();
        _effects.Clear();
    }

}