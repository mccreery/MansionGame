using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TransformData inventoryTransformData;

    [SerializeField]
    private bool overrideForInspector;

    [SerializeField]
    private TransformData inspectorTransformData;

    public TransformData InventoryTransformData => inventoryTransformData;
    public TransformData InspectorTransformData => overrideForInspector ? inspectorTransformData : inventoryTransformData;

    public bool autoInspect = false;

    public TransformData WorldTransformData { get; private set; }

    private void Start()
    {
        WorldTransformData = new TransformData(transform);
    }

    public void Interact(Hotbar hotbar)
    {
        int slot = hotbar.Add(this);

        if (slot == -1)
        {
            FindObjectOfType<Message>().ShowMessage("I'm carrying too much.");
        }
        else if (autoInspect)
        {
            hotbar.SelectedSlot = slot;
            hotbar.Inspect();
        }
    }
}
