using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkAudioManager : NetworkBehaviour {
    [SerializeField] private SettingsConfigSO _settingsCfg;
    [Space]
    [SerializeField] private AudioSource _playerShoot;
    [SerializeField] private AudioSource _hittingPlayer;
    [SerializeField] private AudioSource _playerDie;
    [SerializeField] private AudioSource _hit;
    [SerializeField] private AudioSource _dash;

    public static NetworkAudioManager Instance;
    private Dictionary<Sounds, AudioSource> _sounds = new();

    public void Awake() {
        InitializeSounds();

        Instance = this;
        DontDestroyOnLoad( gameObject );
    }

    private void InitializeSounds() {
        _sounds = new() {
                    { Sounds.Hit, _hit },
                    { Sounds.PlayerShoot, _playerShoot },
                    { Sounds.HittingPlayer, _hittingPlayer },
                    { Sounds.PlayerDie, _playerDie },
                    { Sounds.Dash, _dash }
                };
    }

    [Rpc( SendTo.Server, InvokePermission = RpcInvokePermission.Everyone )]
    public void PlayClipAtPointServerRpc( Sounds type, Vector3 Position ) {
        PlayAudioSourceAtPositionClientRpc( type, Position );
    }
    [ClientRpc]
    public void PlayAudioSourceAtPositionClientRpc( Sounds type, Vector3 position ) {
        if ( _sounds.TryGetValue( type, out AudioSource audioSource ) ) {
            var tempGO = Instantiate( audioSource.gameObject, position, Quaternion.identity );
            tempGO.GetComponent<AudioSource>().volume *= _settingsCfg.EffectsVolume;
            Destroy( tempGO, audioSource.clip.length );
        } else {
            Debug.LogWarning( $"[{nameof( NetworkAudioManager )}] Error when getting audioSource." );
        }
    }
}