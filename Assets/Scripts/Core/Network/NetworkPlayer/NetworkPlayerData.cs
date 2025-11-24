using Unity.Netcode;
using UnityEngine;


[System.Serializable]
public struct NetworkPlayerData : INetworkSerializable {
    public ulong NetID;
    public string Name;
    public Color TeamColor;

    public NetworkPlayerData( ulong netID, Color teamColor, string name ) {
        NetID = netID;
        Name = name;
        TeamColor = teamColor;
    }

    public void NetworkSerialize<T>( BufferSerializer<T> serializer ) where T : IReaderWriter {
        serializer.SerializeValue( ref NetID );
        serializer.SerializeValue( ref TeamColor );
    }
}