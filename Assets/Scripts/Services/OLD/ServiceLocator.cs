//using System;
//using System.Collections.Generic;
//using UnityEngine;

//public static class ServiceLocator {
//    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();
//    private static readonly Dictionary<Type, object> _sceneServices = new Dictionary<Type, object>();

//    public static void Register<T>( T service ) where T : class {
//        Type type = typeof( T );
//        if ( _services.ContainsKey( type ) ) {
//            Debug.LogWarning( $"Service {type.Name} already registered. Overwriting." );
//            _services[ type ] = service;
//        } else {
//            _services.Add( type, service );
//        }
//    }

//    public static void RegisterSceneService<T>( T service ) where T : class {
//        Type type = typeof( T );
//        if ( _sceneServices.ContainsKey( type ) ) {
//            Debug.LogWarning( $"Scene service {type.Name} already registered. Overwriting." );
//            _sceneServices[ type ] = service;
//        } else {
//            _sceneServices.Add( type, service );
//        }
//    }

//    public static T Get<T>() where T : class {
//        Type type = typeof( T );

//        if ( _services.TryGetValue( type, out object service ) ) {
//            return service as T;
//        }

//        if ( _sceneServices.TryGetValue( type, out service ) ) {
//            return service as T;
//        }

//        Debug.LogError( $"Service {type.Name} not found!" );
//        return null;
//    }

//    public static bool TryGet<T>( out T service ) where T : class {
//        service = Get<T>();
//        return service != null;
//    }

//    public static void Unregister<T>() where T : class {
//        Type type = typeof( T );
//        _services.Remove( type );
//    }

//    public static void UnregisterSceneService<T>() where T : class {
//        Type type = typeof( T );
//        _sceneServices.Remove( type );
//    }

//    public static void ClearSceneServices() {
//        _sceneServices.Clear();
//    }

//    public static void ClearAll() {
//        _services.Clear();
//        _sceneServices.Clear();
//    }
//}