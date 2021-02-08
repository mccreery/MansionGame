using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    private PinPoint[] pinnedPoints = new PinPoint[4];
    private int pinCount = 0;

    public bool AllPinsPlaced => pinCount == pinnedPoints.Length;
    public float stringHeight = 0.1f;

    // Pins can be pinned multiple times with this method
    // The pinpoints track this themselves
    public void Pin(PinPoint point)
    {
        int i = Array.IndexOf(pinnedPoints, null);
        if (i != -1)
        {
            pinnedPoints[i] = point;
            ++pinCount;
            UpdateLines();
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
            UpdateLines();
        }
    }

    public LineRenderer[] lineRenderers;

    private void Start()
    {
        UpdateLines();
    }

    private void UpdateLines()
    {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            PinPoint pointA = pinnedPoints[i * 2];
            PinPoint pointB = pinnedPoints[i * 2 + 1];

            Vector3 stringOffset = new Vector3(0, stringHeight);

            if (pointA != null && pointB != null)
            {
                lineRenderers[i].enabled = true;
                lineRenderers[i].SetPositions(new Vector3[] {
                    pointA.transform.position + stringOffset,
                    pointB.transform.position + stringOffset
                });
            }
            else
            {
                lineRenderers[i].enabled = false;
            }
        }
    }
}
