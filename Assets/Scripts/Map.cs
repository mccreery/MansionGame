using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Map : MonoBehaviour
{
    public ItemPickup[] pinPool;

    private void Start()
    {
        // Hide all pins
        foreach (ItemPickup pin in pinPool)
        {
            pin.gameObject.SetActive(false);
        }
    }

    private List<PinPoint> pinnedPoints = new List<PinPoint>();

    public bool AllPinned => pinnedPoints.Count == pinPool.Length;

    public bool AddPin(PinPoint point)
    {
        if (!pinnedPoints.Contains(point))
        {
            pinnedPoints.Add(point);
            return true;
        }
        else
        {
            return false;
        }
    }
}
