using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Vector3 position = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    public Vector3 scale = Vector3.one;

    public bool autoInspect = false;

    public bool overrideForInspector;
    public Vector3 inspectorPosition = Vector3.zero;
    public Quaternion inspectorRotation = Quaternion.identity;
    public Vector3 inspectorScale = Vector3.one;

    public ItemPickup Interact(ItemPickup heldItem)
    {
        Hotbar hotbar = FindObjectOfType<Hotbar>();
        int slot = hotbar.Add(this);

        if (autoInspect)
        {
            hotbar.SelectedSlot = slot;
            hotbar.Inspect();
        }

        // Don't overwrite held item if not this
        if (heldItem != null)
        {
            return heldItem;
        }
        // Don't overwrite added item with null
        else if (slot == hotbar.SelectedSlot)
        {
            return this;
        }
        else
        {
            return null;
        }
    }
}
