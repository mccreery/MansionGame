using UnityEngine;

public class RecordPlayerInteraction : MonoBehaviour, IInteractable
{
    public ItemPickup correctRecord;

    public Message message;
    public GameObject recordInWorld;

    public Cabinet cabinet;

    public void Start()
    {
        recordInWorld.SetActive(false);
    }

    public void Interact(Hotbar hotbar)
    {
        if (hotbar.SelectedItem == correctRecord)
        {
            recordInWorld.SetActive(true);
            cabinet.Smash();
            hotbar.Remove(hotbar.SelectedSlot, false);
        }
        else if (hotbar.SelectedItem != null)
        {
            message.ShowMessage("That doesn't seem right.");
        }
        else
        {
            message.ShowMessage("Looks like a record could go here.");
        }
    }
}
