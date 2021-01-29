using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float yawSpeed = 180;
    public float pitchSpeed = 180;

    public bool invertY;
    public bool repeatYaw = true;

    public bool parentYaw = true;

    private Vector2 eulerAngles;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        eulerAngles.y += Input.GetAxisRaw("Mouse X") * yawSpeed * Time.deltaTime;
        if (repeatYaw)
        {
            eulerAngles.y = Mathf.Repeat(eulerAngles.y + 180, 360) - 180;
        }

        if (invertY)
        {
            // Rotation around the possitive X axis looks down
            eulerAngles.x += Input.GetAxisRaw("Mouse Y") * pitchSpeed * Time.deltaTime;
        }
        else
        {
            // Rotation against the negative X axis looks up
            eulerAngles.x -= Input.GetAxisRaw("Mouse Y") * pitchSpeed * Time.deltaTime;
        }
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, -90, 90);

        if (parentYaw)
        {
            transform.localRotation = Quaternion.Euler(eulerAngles.x, 0, 0);
            transform.parent.localRotation = Quaternion.Euler(0, eulerAngles.y, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(eulerAngles);
        }
    }
}
