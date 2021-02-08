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

    public ItemPickup Interact(ItemPickup heldItem)
    {
        if (heldItem == correctRecord)
        {
            recordInWorld.SetActive(true);
            cabinet.Smash();
            return null;
        }
        else if (heldItem != null)
        {
            message.ShowMessage("That doesn't seem right.");
            return heldItem;
        }
        else
        {
            message.ShowMessage("Looks like a record could go here.");
            return heldItem;
        }
    }
}
