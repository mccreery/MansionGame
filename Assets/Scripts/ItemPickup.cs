using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public Vector3 scale = Vector3.one;

    public ItemPickup Interact(ItemPickup heldItem)
    {
        if (heldItem != null)
        {
            Hotbar hotbar = FindObjectOfType<Hotbar>();
            hotbar.Add(this);
            return heldItem;
        }
        else
        {
            return this;
        }
    }
}
