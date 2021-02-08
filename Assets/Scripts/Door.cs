using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    public Transform hinge;

    public float openRotation = 100.0f;

    private bool open;

    // Prevent animation running at start of game
    private float animationStartTime = Mathf.NegativeInfinity;
    public float animationTime = 1.0f;

    private float AnimationProgress => Mathf.Clamp(Time.time - animationStartTime, 0, animationTime);

    public ItemPickup key;

    public AudioClip lockedClip, unlockClip, openClip, closeClip;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Interact(Hotbar hotbar)
    {
        if (key == null)
        {
            Toggle();
            if (open)
            {
                audioSource.PlayOneShot(openClip);
            }
        }
        else if (hotbar.SelectedItem == key)
        {
            audioSource.PlayOneShot(unlockClip);

            // Unlock door
            Toggle();
            key = null;

            // Consume key
            hotbar.Remove(hotbar.SelectedSlot, returnItem: false);
        }
        else
        {
            audioSource.PlayOneShot(lockedClip);
        }
    }

    public void Toggle()
    {
        open = !open;
        // Mirror the current animation position by restarting the animation in the past
        animationStartTime = Time.time - (animationTime - AnimationProgress);
    }

    private float lastT = 0.0f;

    private void Update()
    {
        float t = Mathf.Clamp01(AnimationProgress / animationTime);
        t = Mathf.SmoothStep(0.0f, 1.0f, t);

        if (!open)
        {
            t = 1.0f - t;
        }

        if (t == 0.0f && lastT > 0.0f)
        {
            audioSource.PlayOneShot(closeClip);
        }
        lastT = t;

        hinge.localRotation = Quaternion.Slerp(Quaternion.identity, Quaternion.Euler(0, openRotation, 0), t);
    }
}
