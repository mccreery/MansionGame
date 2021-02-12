using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FakeKey : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public GameObject realKey;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (rigidbody.velocity.sqrMagnitude < 0.1f)
        {
            Instantiate(realKey, transform.localPosition, transform.localRotation, transform.parent);
            Destroy(gameObject);
        }
    }
}
