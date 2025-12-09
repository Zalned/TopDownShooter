public class ScaleMod : IMod {
    private readonly float _mult;
    private PlayerStats _stats;

    public ScaleMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        _stats = stats;
        float scale = _stats.RuntimeConfig.Bullet.Scale;
        _stats.ApplyBulletModifier( BulletStatType.Scale, scale * _mult );
    }
}

