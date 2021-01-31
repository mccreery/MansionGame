using UnityEngine;

public class Vase : MonoBehaviour, IInteractable
{
    public ItemPickup hammer;
    public string cantUseText;

    public bool Interact(ItemPickup heldItem)
    {
        if (heldItem == hammer)
        {
            Shatter();
        }
        else
        {
            FindObjectOfType<Message>().ShowMessage(cantUseText);
        }
        return false;
    }

    public GameObject brokenVersion;

    private void Start()
    {
        brokenVersion.SetActive(false);
    }

    public void Shatter()
    {
        brokenVersion.SetActive(true);
        gameObject.SetActive(false);
    }
}
