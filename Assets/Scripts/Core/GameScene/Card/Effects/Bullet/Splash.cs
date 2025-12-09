public class SplashMod : IMod {
    public void Install( PlayerStats stats, CardContext _ ) {
        stats.ApplyBulletModifier( BulletStatType.HasSplash, true );
    }
}
