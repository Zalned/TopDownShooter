using System;

public class LifeStealService {
    public void LifeSteal( Action<float> addHealthCallback, float value ) {
        addHealthCallback.Invoke( value );
    }
}