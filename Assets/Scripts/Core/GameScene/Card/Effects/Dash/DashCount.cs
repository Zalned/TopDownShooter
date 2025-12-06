using UnityEngine;

[CreateAssetMenu( fileName = "DashMod", menuName = "PlayerMods/DashCount" )]
public class DashCountSO : ModSO {
    public int Count = 0;

    public override ISimpleMod CreateRuntime() {
        return new DashCountMod( Count );
    }
}

public class DashCountMod : ISimpleMod {
    private readonly int _count;
    private PlayerStats _stats;

    public DashCountMod( int count) {
        _count = count;
    }

    public void Install( PlayerStats stats, CardContext ctx ) {
        _stats = stats;
        _stats.ApplyPlayerModifier( PlayerStatType.DashCount, _count );
    }
}