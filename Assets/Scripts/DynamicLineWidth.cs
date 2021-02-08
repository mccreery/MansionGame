using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DynamicLineWidth : MonoBehaviour
{
    public float unitWidth;
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lineRenderer.widthMultiplier = unitWidth * transform.lossyScale.x;
    }
}
