using System;
using UnityEngine;

public class TablePlace : MonoBehaviour, IInteractable
{
    public TablePuzzle puzzle;

    public ItemPickup placed;

    private void Start()
    {
        if (placed != null)
        {
            MovePlaced();
        }
    }

    public void Interact(Hotbar hotbar)
    {
        if (placed != null)
        {
            if (hotbar.Add(placed) != -1)
            {
                placed = null;
            }
            else
            {
                FindObjectOfType<Message>().ShowMessage("I'm carrying too much.");
            }
        }
        else if (Array.IndexOf(puzzle.People, hotbar.SelectedItem) != -1)
        {
            placed = hotbar.Remove(hotbar.SelectedSlot, true);
            MovePlaced();
            puzzle.UpdateCheck();
        }
    }

    public Transform placeholderPlaceholder;

    private void MovePlaced()
    {
        TransformData transformData = new TransformData();
        transformData.parent = placeholderPlaceholder;
        transformData.localScale = placed.transform.localScale;

        transformData.CopyTo(placed.transform, true);
    }
}
