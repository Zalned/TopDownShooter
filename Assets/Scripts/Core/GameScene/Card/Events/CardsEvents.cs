using System;
using UnityEngine;

public static class CardsEvents {
    public static Action<ServerBullet> OnShoot;
    public static Action<ServerBullet, Collider> OnHit;
}

public record OnPlayerPickCard( ulong playerID, CardSO CardSO );