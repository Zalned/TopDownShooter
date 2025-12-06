using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerModel {
    public PlayerStats PlayerStats = new();
    public ReactiveProperty<float> CurrentHealth;

    private CardContext _ctx;

    public List<IBulletMod> BulletMods => PlayerStats.RuntimeConfig.Bullet.Mods;
    public List<IPlayerMod> PlayerMods => PlayerStats.RuntimeConfig.Player.Mods;

    public void SetCards( List<CardSO> cards ) {
        foreach ( var card in cards ) {
            AddCard( card );
        }
    }

    private void AddCard( CardSO card ) {
        Debug.Log( $"Add card: {card.Name}" );

        foreach ( var effectSO in card.Effects ) {
            ISimpleMod mod = effectSO.CreateRuntime();
            mod.Install( PlayerStats, _ctx );

            if ( mod is IPlayerMod ) {
                PlayerStats.RuntimeConfig.Player.Mods.Add( (IPlayerMod)mod );

            } else if ( mod is IBulletMod ) {
                PlayerStats.RuntimeConfig.Bullet.Mods.Add( (IBulletMod)mod );
            }
        }
    }
}