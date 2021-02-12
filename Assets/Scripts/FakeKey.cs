using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FakeKey : MonoBehaviour
{
    private new Rigidbody rigidbody;
    public GameObject realKey;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        realKey.SetActive(false);
    }

    private bool startedMoving;

    private void FixedUpdate()
    {
        if (!startedMoving)
        {
            if (rigidbody.velocity.sqrMagnitude > 0.1f)
            {
                startedMoving = true;
            }
        }
        else if (rigidbody.velocity.sqrMagnitude < 0.1f)
        {
            realKey.SetActive(true);
            new TransformData(transform).CopyTo(realKey.transform, true);
            Destroy(gameObject);
        }
    }
}
