using UnityEngine;

public class BulletModel {
    public readonly LayerMask HitMask;

    public ulong OwnerID { get; private set; }
    public float Speed => _config.Speed.Value;
    public float Radius => _config.Radius.Value;
    public float Damage => _config.Damage.Value;

    private BulletRuntimeStats _config;
    public float CurrentBounceCount = 0;
    public float CurrentPenetrationCount = 0;

    public BulletModel( BulletRuntimeStats bulletRuntimeStats, ulong ownerId ) {
        _config = bulletRuntimeStats;
        CurrentBounceCount = _config.BounceCount.Value;
        CurrentPenetrationCount = _config.PenetrationCount.Value;

        OwnerID = ownerId;

        HitMask = LayerMask.GetMask(
        Defines.Layers.ENVIROMENT,
        Defines.Layers.PLAYER,
        Defines.Layers.BULLET );
    }
}