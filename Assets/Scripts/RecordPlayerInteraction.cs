using UnityEngine;

public class RecordPlayerInteraction : MonoBehaviour, IInteractable
{
    public ItemPickup correctRecord;

    public Message message;
    public GameObject recordInWorld;

    public void Start()
    {
        recordInWorld.SetActive(false);
    }

    public bool Interact(ItemPickup heldItem)
    {
        if (heldItem == correctRecord)
        {
            recordInWorld.SetActive(true);
            return true;
        }
        else if (heldItem != null)
        {
            message.ShowMessage("That doesn't seem right.");
            return false;
        }
        else
        {
            message.ShowMessage("Looks like a record could go here.");
            return false;
        }
    }
}
