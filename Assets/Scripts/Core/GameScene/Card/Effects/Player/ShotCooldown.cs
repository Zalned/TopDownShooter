using System;

public class AttackSpeedMod : BaseMod, IMod {
    public AttackSpeedMod( StatType type, float value ) : base( type, value ) { }

    public void Install( PlayerContext playerCtx, CardContext _ ) {
        var attackSpeed = playerCtx.Stats.RuntimeConfig.Player.AttackSpeed;

        switch ( type ) {
            case StatType.Additive: attackSpeed.Additive += value; break;
            case StatType.Percent: attackSpeed.Percent += value; break;
            default: throw new ArgumentOutOfRangeException();
        }
        playerCtx.Stats.RuntimeConfig.Player.AttackSpeed = attackSpeed;
    }
}