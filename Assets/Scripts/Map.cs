using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Map : MonoBehaviour
{
    private PinPoint[] pinnedPoints = new PinPoint[4];
    private int pinCount = 0;

    public bool AllPinsPlaced => pinCount == pinnedPoints.Length;

    // Pins can be pinned multiple times with this method
    // The pinpoints track this themselves
    public void Pin(PinPoint point)
    {
        int i = Array.IndexOf(pinnedPoints, null);
        if (i != -1)
        {
            pinnedPoints[i] = point;
            ++pinCount;
        }
    }

    // Only unpins if pinned in the first place
    public void Unpin(PinPoint point)
    {
        int i = Array.IndexOf(pinnedPoints, point);
        if (i != -1)
        {
            pinnedPoints[i] = null;
            --pinCount;
        }
    }
}
