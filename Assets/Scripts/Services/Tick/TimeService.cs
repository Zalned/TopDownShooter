using System;
using System.Collections.Generic;

public class TimerService : ITickable {
    private readonly List<Timer> _timers = new();

    public void Tick() {
        float dt = TickService.TickDeltaTime;

        for ( int i = _timers.Count - 1; i >= 0; i-- ) {
            if ( _timers[ i ].Tick( dt ) )
                _timers.RemoveAt( i );
        }
    }

    public void RunAfter( float seconds, Action action ) {
        _timers.Add( new Timer( seconds, action ) );
    }
}

public class Timer {
    private float _remaining;
    private readonly Action _callback;

    public Timer( float seconds, Action callback ) {
        _remaining = seconds;
        _callback = callback;
    }

    public bool Tick( float dt ) {
        _remaining -= dt;
        if ( _remaining <= 0f ) {
            _callback?.Invoke();
            return true;
        }
        return false;
    }
}
