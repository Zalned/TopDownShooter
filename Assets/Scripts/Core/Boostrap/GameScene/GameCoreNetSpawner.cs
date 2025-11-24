using UnityEngine;

public class GameCoreNetSpawner : MonoBehaviour {
    [SerializeField] private GameObject _gameCoreObject;

    private void Awake() {
        if ( NetcodeHelper.IsServer ) {
            var corePrefab = Instantiate( _gameCoreObject );
            NetcodeHelper.Spawn( corePrefab, true );
        } else {
            Destroy( gameObject );
        }
    }
}