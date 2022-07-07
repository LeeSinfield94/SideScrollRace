using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public const byte MOUSE01 = 0x01;
    public const byte P = 0x02;

    public byte buttons;
    public NetworkBool canMoveForward;
    public Vector3 direction;
}