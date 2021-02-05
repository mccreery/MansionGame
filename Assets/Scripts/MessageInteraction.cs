using UnityEngine;

public class MessageInteraction : MonoBehaviour, IInteractable
{
    public string message;
    public AudioClip sound;

    public bool Interact(ItemPickup helditem)
    {
        FindObjectOfType<Message>().ShowMessage(message);

        if (sound != null)
        {
            AudioSource source = GetComponent<AudioSource>();
            if (source != null)
            {
                source.PlayOneShot(sound);
            }
        }

        return false;
    }
}
