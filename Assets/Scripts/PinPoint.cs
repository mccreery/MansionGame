using UnityEngine;

public class PinPoint : MonoBehaviour, IInteractable
{
    public string doublePinTag;
    public string singlePinTag;

    public GameObject singlePinItemPrefab;

    public ItemPickup Interact(ItemPickup heldItem)
    {
        if (heldItem.CompareTag(doublePinTag))
        {
            GameObject singlePin = Instantiate(singlePinItemPrefab);
            singlePin.tag = singlePinTag;
            return singlePin.GetComponent<ItemPickup>();
        }
        else if (heldItem.CompareTag(singlePinTag))
        {
            return null;
        }
        else
        {
            return heldItem;
        }
    }
}
