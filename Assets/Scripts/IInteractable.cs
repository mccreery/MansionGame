public interface IInteractable
{
    /// <summary>
    ///   Called when the user interacts with this object.
    /// </summary>
    /// <param name="heldItem">The item the player has selected in their inventory.</param>
    /// <returns>true if the held item should be consumed.</returns>
     bool Interact(ItemPickup heldItem);
}
