using UnityEngine;

public class BulletModel {
    public readonly LayerMask HitMask;

    public ulong OwnerID { get; private set; }

    public BulletRuntimeStats Config;
    public float CurrentBounceCount = 0;
    public float CurrentPenetrationCount = 0;

    public BulletModel( BulletRuntimeStats bulletRuntimeStats, ulong ownerId ) {
        Config = bulletRuntimeStats;
        CurrentBounceCount = Config.BounceCount;
        CurrentPenetrationCount = Config.PenetrationCount;

        OwnerID = ownerId;

        HitMask = LayerMask.GetMask(
        Defines.Layers.ENVIROMENT,
        Defines.Layers.PLAYER,
        Defines.Layers.BULLET );
    }
}