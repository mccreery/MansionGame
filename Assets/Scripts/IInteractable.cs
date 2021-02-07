public interface IInteractable
{
    /// <summary>
    ///   Called when the user interacts with this object.
    /// </summary>
    /// <param name="heldItem">The item the player has selected in their inventory.</param>
    /// <returns>The item to replace the selected item with, including heldItem or null.</returns>
    ItemPickup Interact(ItemPickup heldItem);
}
