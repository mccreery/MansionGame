using UnityEngine;

public class Vase : MonoBehaviour, IInteractable
{
    public string cantUseText;

    public bool Interact(ItemPickup heldItem)
    {
        FindObjectOfType<Message>().ShowMessage(cantUseText);
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Shatter();
        }
    }
}
