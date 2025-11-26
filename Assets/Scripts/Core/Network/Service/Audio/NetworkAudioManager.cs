using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkAudioManager : NetworkBehaviour {
    public static NetworkAudioManager Instance;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _playerShoot;
    [SerializeField] private AudioClip _hittingPlayer;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _dash;

    private Dictionary<Sounds, AudioClip> _sounds = new();

    public void Awake() {
        _audioSource = GetComponent<AudioSource>();
        InitializeSounds();

        Instance = this;
        DontDestroyOnLoad( gameObject );
    }

    private void InitializeSounds() {
        _sounds = new Dictionary<Sounds, AudioClip> {
                    { Sounds.Hit, _hit },
                    { Sounds.PlayerShoot, _playerShoot },
                    { Sounds.HittingPlayer, _hittingPlayer },
                    { Sounds.Dash, _dash }
                };
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void PlayClipAtPointServerRpc( Sounds type, Vector3 Position ) {
        //PlayClipAtPointClientRpc( type, Position );
    }
    [ClientRpc]
    private void PlayClipAtPointClientRpc( Sounds type, Vector3 Position ) {
        if ( _sounds.TryGetValue( type, out AudioClip clip ) ) {
            AudioSource.PlayClipAtPoint( clip, Position );
        } else {
            Debug.LogWarning( $"[{nameof( NetworkAudioManager )}] Error when getting audioClip." );
        }
    }
}