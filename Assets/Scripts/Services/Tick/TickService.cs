using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TickService : ITickService {
    private readonly List<ITickable> _tickables = new();
    private const int DEFAULT_TICKRATE = 60;
    public static float TickDeltaTime { get; private set; }
    public static event Action OnTick;

    public TickService() {
        if ( NetworkManager.Singleton != null ) {
            NetworkManager.Singleton.NetworkTickSystem.Tick += Tick;
            TickDeltaTime = 1f / NetworkManager.Singleton.NetworkTickSystem.TickRate;
        } else { 
            Debug.LogError( $"[{nameof( TickService )}] NetworkManager is null." ); 
            TickDeltaTime *= DEFAULT_TICKRATE;
        }
    }

    public void Register( ITickable tickable ) {
        if ( !_tickables.Contains( tickable ) ) {
            _tickables.Add( tickable );
        } else { LogDuplicateReference( tickable ); }
    }

    public void Unregister( ITickable tickable ) {
        if ( _tickables.Contains( tickable ) ) {
            _tickables.Remove( tickable );
        } else { TickableNotRegistredReference( tickable ); }
    }

    public void Tick() {
        for ( int i = 0; i < _tickables.Count; i++ ) {
            _tickables[ i ].Tick();
            OnTick?.Invoke();
        }
    }

    public void LogDuplicateReference( ITickable tickable ) {
        Debug.LogWarning( $"[{nameof( TickService )}] Tickables already contains: {tickable.DescribeTickable()}" );
    }
    public void TickableNotRegistredReference( ITickable tickable ) {
        Debug.LogWarning( $"[{nameof( TickService )}] Tickables not contains: {tickable.DescribeTickable()}" );
    }
}