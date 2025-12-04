using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {
    [SerializeField] private GameObject _mapPrefab;
    [SerializeField] private List<GameObject> _spawnPoints;

    public GameObject MapPrefab => _mapPrefab;
    public List<GameObject> SpawnPoints => _spawnPoints;
}