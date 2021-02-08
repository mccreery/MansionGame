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

    public void Interact(Hotbar hotbar)
    {
        if (Pinned)
        {
            // Removing last pin gives back pin item
            if (map.AllPinsPlaced)
            {
                hotbar.Add(pinItem);
            }

            Pinned = false;
            map.Unpin(this);
        }
        else if (hotbar.SelectedItem == pinItem)
        {
            Pinned = true;
            map.Pin(this);

            // Adding last pin takes pin item away
            if (map.AllPinsPlaced)
            {
                hotbar.Remove(hotbar.SelectedSlot, false);
            }
        }
        else
        {
            FindObjectOfType<Message>().ShowMessage(message);
        }
    }
}
