using Unity.Netcode;
using UnityEngine;

public class NetworkSpawner : NetworkBehaviour {
    public static NetworkSpawner Instance;

    public override void OnNetworkSpawn() {
        Instance = this;
    }

    public GameObject NetworkSpawnObject( GameObject go ) {
        var obj = Instantiate( go );
        NetcodeHelper.Spawn( obj, true );
        return obj;
    }

    public void NetworkDespawnObject( GameObject go ) { 
        NetcodeHelper.Despawn( go );
    }
}