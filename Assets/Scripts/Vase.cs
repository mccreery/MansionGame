using UnityEngine;

public class Vase : MonoBehaviour, IInteractable
{
    public string cantUseText;

    public void Interact()
    {
        FindObjectOfType<Message>().ShowMessage(cantUseText);
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
