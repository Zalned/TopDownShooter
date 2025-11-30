using System;
using System.Collections.Generic;

public static class EventBus {
    private static readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    // Подписка возвращает IDisposable, чтобы потом отписаться
    public static IDisposable Subscribe<T>( Action<T> callback ) {
        var type = typeof( T );
        if ( !_subscribers.ContainsKey( type ) )
            _subscribers[ type ] = new List<Delegate>();

        _subscribers[ type ].Add( callback );

        return new Subscription<T>( callback, () => Unsubscribe( callback ) );
    }

    private static void Unsubscribe<T>( Action<T> callback ) {
        var type = typeof( T );
        if ( _subscribers.TryGetValue( type, out var list ) ) {
            list.Remove( callback );
            if ( list.Count == 0 )
                _subscribers.Remove( type );
        }
    }

    public static void Publish<T>( T evt ) {
        var type = typeof( T );
        if ( _subscribers.TryGetValue( type, out var list ) ) {
            // Создаем копию, чтобы избежать изменений списка во время перебора
            foreach ( var del in list.ToArray() ) {
                ((Action<T>)del)?.Invoke( evt );
            }
        }
    }

    private class Subscription<T> : IDisposable {
        private Action<T> _callback;
        private Action _unsubscribeAction;

        public Subscription( Action<T> callback, Action unsubscribeAction ) {
            _callback = callback;
            _unsubscribeAction = unsubscribeAction;
        }

        public void Dispose() {
            _unsubscribeAction?.Invoke();
            _callback = null;
            _unsubscribeAction = null;
        }
    }
}
