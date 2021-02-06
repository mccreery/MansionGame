using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Cabinet : MonoBehaviour
{
    public GameObject fixedObject, brokenObject;
    public AudioClip smashClip;

    public void Smash()
    {
        fixedObject.SetActive(false);
        brokenObject.SetActive(true);
        GetComponent<AudioSource>().PlayOneShot(smashClip);
    }
}
