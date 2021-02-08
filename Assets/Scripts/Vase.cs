using UnityEngine;

public class Vase : MonoBehaviour, IInteractable
{
    public ItemPickup hammer;
    public string cantUseText;

    public void Interact(Hotbar hotbar)
    {
        if (hotbar.SelectedItem == hammer)
        {
            Shatter();
        }
        else
        {
            FindObjectOfType<Message>().ShowMessage(cantUseText);
        }
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
