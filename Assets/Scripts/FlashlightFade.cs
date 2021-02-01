using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlashlightFade : MonoBehaviour
{
    public Reticle reticle;
    public Ranges.MapParameters mapDistanceToIntensity;

    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        float distance;
        if (reticle.Hit)
        {
            distance = reticle.Raycast.distance;
        }
        else
        {
            distance = Mathf.Max(mapDistanceToIntensity.fromA, mapDistanceToIntensity.fromB);
        }

        light.intensity = Ranges.MapRange(mapDistanceToIntensity, distance);
    }
}
