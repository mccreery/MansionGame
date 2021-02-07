using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Globe : MonoBehaviour, IInteractable
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool Interact(ItemPickup heldItem)
    {
        if (allowInteraction)
        {
            allowInteraction = false;
            animator.Play("Spin", -1, 0);
        }
        return false;
    }

    private bool allowInteraction = true;
    public void AllowInteraction() => allowInteraction = true;
}
