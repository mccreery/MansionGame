using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public TransformData inventoryTransformData;

    public bool overrideForInspector;
    public TransformData inspectorTransformData;

    public bool autoInspect = false;

    public void Interact(Hotbar hotbar)
    {
        int slot = hotbar.Add(this);

        if (autoInspect)
        {
            hotbar.SelectedSlot = slot;
            hotbar.Inspect();
        }
    }
}
