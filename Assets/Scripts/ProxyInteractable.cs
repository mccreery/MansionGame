using UnityEngine;
using UnityEngine.Events;

public class ProxyInteractable : MonoBehaviour, IInteractable
{
    public UnityEvent<Hotbar> interact;

    public void Interact(Hotbar hotbar)
    {
        interact.Invoke(hotbar);
    }
}
