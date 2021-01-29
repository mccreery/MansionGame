using UnityEngine;

public class ProxyInteractable : MonoBehaviour, IInteractable
{
    public GameObject target;

    public void Interact()
    {
        target.GetComponent<IInteractable>().Interact();
    }
}
