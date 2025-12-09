using UnityEngine;

[CreateAssetMenu( fileName = "BounceMod", menuName = "BulletMods/Bounce" )]
public class BounceSO : ModSO {
    public int Count = 0;

    public override IMod CreateRuntime() {
        return new BounceMod( Count );
    }
}

public class BounceMod : IMod {
    private readonly float _count;
    private PlayerStats _stats;

    public BounceMod( int count ) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats.ApplyBulletModifier( BulletStatType.RicochetCount, _count );
    }
}