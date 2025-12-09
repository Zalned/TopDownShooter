using UnityEngine;

[CreateAssetMenu( fileName = "SplashMod", menuName = "BulletMods/Splash" )]
public class SplashSO : ModSO {
    public override IMod CreateRuntime() {
        return new SplashMod();
    }
}

public class SplashMod : IMod {
    public void Install( PlayerStats stats, CardContext _ ) {
        stats.ApplyBulletModifier( BulletStatType.HasSplash, true );
    }
}
