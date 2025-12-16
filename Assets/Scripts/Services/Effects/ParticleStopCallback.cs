using System;
using UnityEngine;

public sealed class ParticleStopCallback : MonoBehaviour {
    private Action _onStopped;

    public void Initialize( Action onStopped ) {
        _onStopped = onStopped;
    }

    private void OnParticleSystemStopped() {
        _onStopped?.Invoke();
    }
}
