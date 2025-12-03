using System;
using UnityEngine;

public static class CardProvideEvents { 
    public static Action<ServerBullet> OnShoot;
    public static Action<ServerBullet, Collider> OnHit;
    public static Action OnDash;
}

public record PlayerCardPickEvent( ulong playerID, ushort CardId );