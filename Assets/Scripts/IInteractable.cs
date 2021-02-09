using System;

public interface IInteractable
{
    void Interact(Hotbar hotbar);
}

public class Interactable : IInteractable
{
    private Action<Hotbar> interact;

    public Interactable(Action<Hotbar> interact)
    {
        this.interact = interact;
    }

    public void Interact(Hotbar hotbar) => interact(hotbar);
}
