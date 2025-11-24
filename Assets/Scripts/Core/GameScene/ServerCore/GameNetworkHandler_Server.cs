using UnityEngine;
using Unity.Netcode;
using Zenject;

public class GameNetworkHandler_Server : NetworkBehaviour {
    private SceneLoader _sceneLoader;

    public void Initialize( SceneLoader sceneLoader ) {
        _sceneLoader = sceneLoader;

        if ( !NetcodeHelper.IsServer ) {
            Debug.LogWarning( $"[{nameof( GameNetworkHandler_Server )}] Is not server, destroying." );
            Destroy( this );
        }
    }

    public void QuitToMenuForClients() {
        if ( NetcodeHelper.IsHost || NetcodeHelper.IsServer ) {
            QuitToMenuClientRpc();
        }
    }

    [ClientRpc]
    private void QuitToMenuClientRpc() {
        _sceneLoader.LoadMenuScene();
    }
}
