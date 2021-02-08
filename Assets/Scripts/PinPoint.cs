using UnityEngine;

public class PinPoint : MonoBehaviour, IInteractable
{
    public GameObject singlePinItemPrefab;

    public ItemPickup Interact(ItemPickup heldItem)
    {
        if (heldItem is PinItem pinItem)
        {
            if (pinItem.hasTwo)
            {
                GameObject singlePin = Instantiate(singlePinItemPrefab);
                return singlePin.GetComponent<ItemPickup>();
            }
            else
            {
                return null;
            }
        }
        else
        {
            return heldItem;
        }
    }
}
