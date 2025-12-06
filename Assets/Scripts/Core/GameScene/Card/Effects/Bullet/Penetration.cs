using UnityEngine;

[CreateAssetMenu( fileName = "PenetrationMod", menuName = "BulletMods/Penetration" )]
public class PenetrationSO : ModSO {
    public int Count = 0;

    public override ISimpleMod CreateRuntime() {
        return new BounceMod( Count );
    }
}

public class PenetrationMod : ISimpleMod {
    private readonly float _count;
    private PlayerStats _stats;

    public PenetrationMod( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats.ApplyBulletModifier( BulletStatType.PenetrationCount, _count );
    }

    public void Uninstall() { }
}