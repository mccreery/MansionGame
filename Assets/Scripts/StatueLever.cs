using UnityEngine;

[RequireComponent(typeof(Animator))]
public class StatueLever : MonoBehaviour, IInteractable
{
    private Animator animator;
    public string openState;

    public Animator bookshelfAnimator;
    public string bookshelfOpenState;

    public bool Activated { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(Hotbar hotbar)
    {
        animator.Play(openState);
        bookshelfAnimator.Play(bookshelfOpenState);

        Activated = true;
    }
}
