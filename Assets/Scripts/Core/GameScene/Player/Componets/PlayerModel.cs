using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class PlayerModel {
    public PlayerStats PlayerStats { get; private set; } = new();
    public ReactiveProperty<float> CurrentHealth;

    private ulong _playerId;
    private CardContext _ctx; // MyTodo: Ќужно получать или инициализировать здесь

    public PlayerModel( ulong playerId ) {
        _playerId = playerId;
        EventBus.Publish( new PlayerStatsChanged( _playerId, PlayerStats ) );
    }

    public List<IBulletMod> BulletMods => PlayerStats.RuntimeConfig.Bullet.Mods;
    public List<IPlayerMod> PlayerMods => PlayerStats.RuntimeConfig.Player.Mods;

    public void SetCards( List<CardData> cards ) {
        PlayerStats.RuntimeConfig.Player.Reset();
        PlayerStats.RuntimeConfig.Bullet.Reset();

        foreach ( var card in cards ) {
            AddCard( card );
        }
    }

    private void AddCard( CardData card ) {
        Debug.Log( $"Add card: {card.Title}" );

        foreach ( var mod in card.Mods ) {
            mod.Install( PlayerStats, _ctx );

            if ( mod is IPlayerMod ) {
                PlayerStats.RuntimeConfig.Player.Mods.Add( (IPlayerMod)mod );

            } else if ( mod is IBulletMod ) {
                PlayerStats.RuntimeConfig.Bullet.Mods.Add( (IBulletMod)mod );
            }
        }
        EventBus.Publish( new PlayerStatsChanged( _playerId, PlayerStats ) );
    }
}