using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Keypad : MonoBehaviour
{
    public TextMesh textMesh;
    public string correctCode;

    public AudioClip accessDenied;
    public Animator vaultDoor;
    public string vaultState;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        textMesh.text = "";
    }

    public void PressButton(int button)
    {
        textMesh.text += button;
    }
}
