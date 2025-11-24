using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkAudioManager : NetworkBehaviour {
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _playerShoot;
    [SerializeField] private AudioClip _hittingPlayer;
    [SerializeField] private AudioClip _hit;

    private Dictionary<Sounds, AudioClip> _sounds = new();

    public void Construct() {
        _audioSource = GetComponent<AudioSource>();
        InitializeSounds();
    }

    private void InitializeSounds() {
        _sounds = new Dictionary<Sounds, AudioClip> {
                    { Sounds.Hit, _hit },
                    { Sounds.PlayerShoot, _playerShoot },
                    { Sounds.HittingPlayer, _hittingPlayer }
                };
    }

    [ServerRpc]
    public void PlayClipAtPointServerRpc( Sounds type, Vector3 Position ) {
        PlayClipAtPointClientRpc( type, Position );
    }
    [ClientRpc]
    private void PlayClipAtPointClientRpc( Sounds type, Vector3 Position ) {
        if ( _sounds.TryGetValue( type, out AudioClip clip ) ) {
            AudioSource.PlayClipAtPoint( clip, Position );
        } else {
            Debug.LogWarning($"[{nameof(NetworkAudioManager)}] Error when getting audioClip.");
        }
    }
}