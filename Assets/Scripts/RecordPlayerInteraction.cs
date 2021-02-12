using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class RecordPlayerInteraction : MonoBehaviour, IInteractable
{
    public ItemPickup correctRecord;

    public Message message;
    public GameObject recordInWorld;

    public Cabinet cabinet;

    private AudioSource audioSource;
    private Animator animator;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        recordInWorld.SetActive(false);
    }

    public void Interact(Hotbar hotbar)
    {
        if (hotbar.SelectedItem is ItemRecord record)
        {
            recordInWorld.SetActive(true);
            animator.Play(record.recordPlayerAnimationState, -1, 0);
        }
        else
        {
            message.ShowMessage("It's stuck on maximum volume.");
        }

        //if (hotbar.SelectedItem == correctRecord)
        //{
        //    recordInWorld.SetActive(true);
        //    cabinet.Smash();
        //    hotbar.Remove(hotbar.SelectedSlot, false);
        //}
        //else if (hotbar.SelectedItem != null)
        //{
        //    message.ShowMessage("That doesn't seem right.");
        //}
        //else
        //{
        //    message.ShowMessage("Looks like a record could go here.");
        //}
    }

    public void PlayRecord(AudioClip song)
    {
        audioSource.Stop();
        audioSource.clip = song;
        audioSource.Play();
    }
}
