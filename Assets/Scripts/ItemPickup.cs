using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        FindObjectOfType<Hotbar>().Add(gameObject);
    }
}
