using UnityEngine;

public class PinPoint : MonoBehaviour, IInteractable
{
    public ItemPickup pinItem;

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
        Pinned = !Pinned;
        return heldItem;
    }
}
