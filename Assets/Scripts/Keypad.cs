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

    public float timeoutDuration = 5f;
    private int wrongGuesses;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        textMesh.text = "";
    }

    public void PressButton(int button)
    {
        if (!timedOut)
        {
            textMesh.text += button;

            if (textMesh.text.Length == 4)
            {
                if (textMesh.text == correctCode)
                {
                    // Open vault
                    vaultDoor.Play(vaultState, -1, 0);
                }
                else
                {
                    audioSource.PlayOneShot(accessDenied);

                    // Exponential timeout
                    float duration = timeoutDuration * (1 << wrongGuesses);
                    ++wrongGuesses;

                    timeout = Time.time + duration;
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(accessDenied);
        }
    }

    private float timeout;
    private bool timedOut;

    private void Update()
    {
        if (Time.time < timeout)
        {
            timedOut = true;
            textMesh.text = Mathf.CeilToInt(timeout - Time.time).ToString().PadRight(4, '-');
        }
        else if (timedOut)
        {
            textMesh.text = "";
            timedOut = false;
        }
    }
}
