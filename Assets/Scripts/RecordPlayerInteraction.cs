using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class RecordPlayerInteraction : MonoBehaviour, IInteractable
{
    public Message message;
    public Cabinet cabinet;
    public Transform spindle;

    private AudioSource audioSource;
    private Animator animator;

    private ItemRecord currentRecord;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void Interact(Hotbar hotbar)
    {
        if (currentRecord != null)
        {
            hotbar.Add(currentRecord);
            currentRecord = null;
            audioSource.Stop();

            animator.Play("NoMovement");
        }
        else if (hotbar.SelectedItem is ItemRecord record)
        {
            hotbar.Remove(hotbar.SelectedSlot, true);

            currentRecord = record;

            record.transform.SetParent(spindle);
            record.transform.localPosition = Vector3.zero;
            record.transform.localRotation = Quaternion.identity;
            record.transform.localScale = Vector3.one;

            animator.Play(record.recordPlayerAnimationState, -1, 0);
        }
        else
        {
            message.ShowMessage("It's stuck on maximum volume.");
        }
    }

    public void PlayRecord(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
    }

    public void Smash() => cabinet.Smash();
}
