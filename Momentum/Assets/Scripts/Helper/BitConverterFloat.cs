using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class BitConverterFloat
{
    /// <summary>
    /// Convert a float to network order
    /// </summary>
    /// <param name="host">Float to convert</param>
    /// <returns>Float in network order</returns>
    public static byte[] HostToNetworkOrder(float host)
    {
        byte[] bytes = BitConverter.GetBytes(host);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return bytes;
    }

    /// <summary>
    /// Convert a float to host order
    /// </summary>
    /// <param name="network">Float to convert</param>
    /// <returns>Float in host order</returns>
    public static float NetworkToHostOrder(byte[] value, int startIndex)
    {
        byte[] bytes = new byte[4];
        Array.Copy(value, startIndex, bytes, 0, 4);

        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);

        return BitConverter.ToSingle(bytes, 0);
    }
}
