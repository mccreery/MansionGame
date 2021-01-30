using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public PlayerData playerData;
    public GameObject hinge;

    public float openRotation = 100.0f;

    private bool open;

    // Prevent animation running at start of game
    private float animationStartTime = Mathf.NegativeInfinity;
    public float animationTime = 1.0f;

    private float AnimationProgress => Mathf.Clamp(Time.time - animationStartTime, 0, animationTime);

    public ItemPickup key;

    public bool Interact(ItemPickup heldItem)
    {
        if (key == null)
        {
            Toggle();
        }
        else if (heldItem == key)
        {
            // Unlock door
            Toggle();
            key = null;
            return true;
        }
        return false;
    }

    public void Toggle()
    {
        open = !open;
        // Mirror the current animation position by restarting the animation in the past
        animationStartTime = Time.time - (animationTime - AnimationProgress);
    }

    private void Update()
    {
        float t = Mathf.Clamp01(AnimationProgress / animationTime);
        t = Mathf.SmoothStep(0.0f, 1.0f, t);

        if (!open)
        {
            t = 1.0f - t;
        }

        hinge.transform.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, openRotation, 0), t);
    }
}
