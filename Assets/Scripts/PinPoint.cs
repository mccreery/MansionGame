using UnityEngine;

public class PinPoint : MonoBehaviour, IInteractable
{
    public Map map;
    public ItemPickup pinItem;

    public string message;

    public GameObject pinObject;
    private bool Pinned
    {
        get => pinObject.activeSelf;
        set => pinObject.SetActive(value);
    }

    private void Start()
    {
        Pinned = false;
    }

    public ItemPickup Interact(ItemPickup heldItem)
    {
        if (Pinned)
        {
            // Removing last pin gives back pin item
            if (map.AllPinsPlaced)
            {
                // if the slot is null we cannot return null because it will override the new item
                if (heldItem == null)
                {
                    heldItem = pinItem;
                }
                else
                {
                    FindObjectOfType<Hotbar>().Add(pinItem);
                }
            }

            Pinned = false;
            map.Unpin(this);
        }
        else if (heldItem == pinItem)
        {
            Pinned = true;
            map.Pin(this);

            // Adding last pin takes pin item away
            if (map.AllPinsPlaced)
            {
                return null;
            }
        }
        else
        {
            FindObjectOfType<Message>().ShowMessage(message);
        }
        return heldItem;
    }
}
