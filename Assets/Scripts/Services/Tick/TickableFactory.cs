using System;

public static class TickableFactory {
    public static T CreateTickable<T>( ITickService tickService, Func<T> creator ) {
        T instance = creator();
        if ( instance is ITickable tickable ) {
            tickService.Register( tickable );
        }
        return instance;
    }
}