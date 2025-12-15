using System;

public class LifeStealService {
    private Action<float> _addHealthCallback;

    public LifeStealService( Action<float> addHealthCallback ) {
        _addHealthCallback = addHealthCallback;
    }

    public void LifeSteal( float value ) {
        _addHealthCallback.Invoke( value );
    }
}