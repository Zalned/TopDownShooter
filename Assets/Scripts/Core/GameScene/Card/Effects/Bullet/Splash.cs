using UnityEngine;

[CreateAssetMenu( fileName = "SplashMod", menuName = "BulletMods/Splash" )]
public class SplashSO : ModSO {
    public override ISimpleMod CreateRuntime() {
        return new SplashMod();
    }
}

public class SplashMod : ISimpleMod {
    public void Install( PlayerStats stats, CardContext _ ) {
        stats.ApplyBulletModifier( BulletStatType.HasSplash, true );
    }
}
