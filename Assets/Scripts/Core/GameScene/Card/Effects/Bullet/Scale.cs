public class ScaleMod : IMod {
    private readonly float _mult;

    public ScaleMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float scale = stats.RuntimeConfig.Bullet.Scale;
        stats.ApplyBulletModifier( BulletStatType.Scale, scale * _mult );
    }
}

