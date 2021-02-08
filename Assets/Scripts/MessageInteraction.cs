using UnityEngine;

public class MessageInteraction : MonoBehaviour, IInteractable
{
    public string message;
    public AudioClip sound;

    public void Interact(Hotbar hotbar)
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
    }
}
