using UnityEngine;
using Unity.Netcode;
using System;

public static class NetcodeHelper {
    public static bool IsServer => NetworkManager.Singleton && NetworkManager.Singleton.IsServer;
    public static bool IsClient => NetworkManager.Singleton && NetworkManager.Singleton.IsClient;
    public static bool IsHost => NetworkManager.Singleton && NetworkManager.Singleton.IsHost;
    public static ulong LocalClientId => NetworkManager.Singleton ? NetworkManager.Singleton.LocalClientId : 0;

    public static void SpawnWithOwnership( GameObject go, ulong clientId, bool destroyWithScene = false ) {
        var netObj = Validate( go );
        CheckOnAlreadySpawned( netObj );
        netObj.SpawnWithOwnership( clientId, destroyWithScene );
    }

    public static void SpawnAsPlayerObject( GameObject go, ulong clientId, bool destroyWithScene = false ) {
        var netObj = Validate( go );
        CheckOnAlreadySpawned( netObj );
        netObj.SpawnAsPlayerObject( clientId, destroyWithScene );
    }

    public static ulong GetOwnerClientID( GameObject go ) {
        var netObj = Validate( go );
        CheckOnNotSpawned( netObj );
        return netObj.OwnerClientId;
    }

    public static void Spawn( GameObject go, bool destroyWithScene = false ) {
        var netObj = Validate( go );
        CheckOnAlreadySpawned( netObj );
        netObj.Spawn( destroyWithScene );
    }

    public static void Despawn( GameObject go, bool destroy = true ) {
        var netObj = Validate( go );
        CheckOnNotSpawned( netObj );
        netObj.Despawn( destroy );
    }

    private static void CheckOnAlreadySpawned( NetworkObject netObj ) {
        if ( netObj.IsSpawned ) { throw new InvalidOperationException( "Object already spawned" ); }
    }
    private static void CheckOnNotSpawned( NetworkObject netObj ) {
        if ( !netObj.IsSpawned ) { throw new InvalidOperationException( "Object is not spawned" ); }
    }

    private static NetworkObject Validate( GameObject go ) {
        if ( !NetworkManager.Singleton ) { throw new InvalidOperationException( "NetworkManager not found" ); }
        if ( !go ) { throw new System.NullReferenceException( "GameObject is null" ); }
        if ( !go.TryGetComponent( out NetworkObject obj ) ) { throw new MissingComponentException( "Missing NetworkObject" ); }

        return obj;
    }
}