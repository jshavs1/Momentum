using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extensions
{
    public static byte[] Serialize(this Vector3 v)
    {
        byte[] mem = new byte[4 * 3],
            xBin = BitConverterFloat.HostToNetworkOrder(v.x),
            yBin = BitConverterFloat.HostToNetworkOrder(v.y),
            zBin = BitConverterFloat.HostToNetworkOrder(v.z);

        xBin.CopyTo(mem, 0);
        yBin.CopyTo(mem, 4);
        zBin.CopyTo(mem, 8);

        return mem;
    }

    public static void Deserialize(this Vector3 v, byte[] bin)
    {
        v.x = BitConverterFloat.NetworkToHostOrder(bin, 0);
        v.y = BitConverterFloat.NetworkToHostOrder(bin, 4);
        v.z = BitConverterFloat.NetworkToHostOrder(bin, 8);
    }
}
