using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FadeDespawn : MonoBehaviour
{
    public FloatGradient fade;

    private new Renderer renderer;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
        renderer = GetComponent<Renderer>();
        RandomizeStop(ref fade.stops[0]);
    }

    private void RandomizeStop(ref FloatGradient.Stop stop)
    {
        stop.time += Random.Range(-stop.time / 2.0f, stop.time / 2.0f);
    }

    private void Update()
    {
        float time = Time.time - startTime;

        if (time > fade.TotalTime)
        {
            Destroy(gameObject);
        }
        else
        {
            Color color = Color.white;
            color.a = fade[time];

            renderer.material.color = color;
        }
    }
}
