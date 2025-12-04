using System.Collections.Generic;
using UnityEngine;

public class MapService : MonoBehaviour {
    [SerializeField] private List<GameMap> _maps;
    private List<int> _usedSpawnPosition = new();

    private GameMap _currentMap = null;
    private GameObject _currentMapObj;

    public void LoadRandomMap() {
        if ( _currentMap == null ) {
            if ( _maps == null ) { Debug.LogError( "Maps in null." ); }

            int randomIndex = Random.Range( 0, _maps.Count );
            _currentMap = _maps[ randomIndex ];

            _currentMapObj = Object.Instantiate( _currentMap.MapPrefab );
            NetcodeHelper.Spawn( _currentMapObj, true );
        } else {
            DespawnMap();
            LoadRandomMap();
        }
    }

    public void DespawnMap() {
        if ( _currentMap != null ) {
            _currentMap = null;
            NetcodeHelper.Despawn( _currentMapObj, true );
        } else {
            Debug.LogWarning( $"[{nameof( MapService )}] No map to despawn." );
        }
    }

    public Vector3 GetRandomSpawnPosition() {
        int spawnPointsCount = _currentMap.SpawnPoints.Count;

        while ( true ) {
            int randomIndex = Random.Range( 0, spawnPointsCount );

            if ( _usedSpawnPosition.Count >= spawnPointsCount ) {
                _usedSpawnPosition.Clear();
            }

            if ( !_usedSpawnPosition.Contains( randomIndex ) ) {
                _usedSpawnPosition.Add( randomIndex );
                return _currentMap.SpawnPoints[ randomIndex ].transform.position;
            }
        }
    }
}
