using UnityEngine;

public class ProxyInteractable : MonoBehaviour, IInteractable
{
    public GameObject target;

    public bool Interact(ItemPickup heldItem)
    {
        return target.GetComponent<IInteractable>().Interact(heldItem);
    }
}
