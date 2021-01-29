using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject hinge;

    public float openRotation = 100.0f;

    private bool open;

    // Prevent animation running at start of game
    private float animationStartTime = Mathf.NegativeInfinity;
    public float animationTime = 1.0f;

    private void OnMouseDown() => Toggle();

    public void Toggle()
    {
        open = !open;
        animationStartTime = Time.time;
    }

    private void Update()
    {
        float t = Mathf.Clamp01((Time.time - animationStartTime) / animationTime);
        t = Mathf.SmoothStep(0.0f, 1.0f, t);

        if (!open)
        {
            t = 1.0f - t;
        }

        hinge.transform.rotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, openRotation, 0), t);
    }
}
