using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public struct HudPlayerData : INetworkSerializable {
    public FixedString64Bytes PlayerText;
    public Color PlayerColor;

    public void NetworkSerialize<T>( BufferSerializer<T> serializer ) where T : IReaderWriter {
        serializer.SerializeValue( ref PlayerText );

        float r = PlayerColor.r;
        float g = PlayerColor.g;
        float b = PlayerColor.b;
        float a = PlayerColor.a;

        serializer.SerializeValue( ref r );
        serializer.SerializeValue( ref g );
        serializer.SerializeValue( ref b );
        serializer.SerializeValue( ref a );

        if ( serializer.IsReader ) {
            PlayerColor = new Color( r, g, b, a );
        }
    }
}

public struct HudPlayerDataArray : INetworkSerializable {
    public HudPlayerData[] Data;

    public void NetworkSerialize<T>( BufferSerializer<T> serializer ) where T : IReaderWriter {
        int count = Data?.Length ?? 0;
        serializer.SerializeValue( ref count );

        if ( serializer.IsReader ) {
            Data = new HudPlayerData[ count ];
        }

        for ( int i = 0; i < count; i++ ) {
            serializer.SerializeValue( ref Data[ i ] );
        }
    }
}
