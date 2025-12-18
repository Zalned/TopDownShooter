using Unity.Netcode;
using UnityEngine;

public class NetworkEventInvoker : NetworkBehaviour {
    public override void OnNetworkSpawn() {
        if ( IsServer ) {
            ProvidersClientEvents.PlayerSpawnedClientEventProvider += OnPlayerSpawned;
        }
    }

    public override void OnNetworkDespawn() {
        if ( IsServer ) {
            ProvidersClientEvents.PlayerSpawnedClientEventProvider -= OnPlayerSpawned;
        }
    }

    private void OnPlayerSpawned() {
        RaisePlayerSpawnedClientRpc();
    }

    [ClientRpc]
    private void RaisePlayerSpawnedClientRpc() {
        ClientEventBridge.RaisePlayerSpawned();
    }
}