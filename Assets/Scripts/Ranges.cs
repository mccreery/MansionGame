using System;
using UnityEngine;

public static class Ranges
{
    [Serializable]
    public struct MapParameters
    {
        public float fromA, fromB;
        public float toA, toB;
    }

    public static float MapRange(MapParameters ranges, float value)
    {
        return MapRange(ranges.fromA, ranges.fromB, ranges.toA, ranges.toB, value);
    }

    public static float MapRange(float fromA, float fromB, float toA, float toB, float value)
    {
        return Mathf.Lerp(toA, toB, Mathf.InverseLerp(fromA, fromB, value));
    }
}
