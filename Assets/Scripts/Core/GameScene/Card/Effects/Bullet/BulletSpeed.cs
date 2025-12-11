public class BulletSpeedMod : IMod {
    private readonly float _mult;

    public BulletSpeedMod( float mult ) {
        _mult = mult;
    }

    public void Install( PlayerStats stats, CardContext _ ) {
        float speed = stats.RuntimeConfig.Bullet.Speed;
        stats.ApplyBulletModifier( BulletStatType.Speed, speed * _mult );
    }
}

