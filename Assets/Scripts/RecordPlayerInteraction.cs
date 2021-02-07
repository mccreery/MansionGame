using UnityEngine;

public class RecordPlayerInteraction : MonoBehaviour, IInteractable
{
    public ItemPickup correctRecord;

    public Message message;
    public GameObject recordInWorld;
    public GameObject cabinetGlass;

    public void Start()
    {
        recordInWorld.SetActive(false);
    }

    public ItemPickup Interact(ItemPickup heldItem)
    {
        if (heldItem == correctRecord)
        {
            recordInWorld.SetActive(true);
            cabinetGlass.SetActive(false);
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
