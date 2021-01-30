using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public Vector3 scale = Vector3.one;

    public bool Interact(ItemPickup heldItem)
    {
        FindObjectOfType<Hotbar>().Add(this);
        return false;
    }
}
