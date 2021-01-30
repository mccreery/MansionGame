using UnityEngine;

public class Inspector : MonoBehaviour
{
    private ItemPickup item;
    public ItemPickup Item
    {
        get => item;
        set
        {
            item = value;

            // Remove previous item
            if (gameObject.transform.childCount > 0)
            {
                Destroy(gameObject.transform.GetChild(0).gameObject);
            }

            // Clone new item for viewing
            viewedObject = Instantiate(item.gameObject, transform);
            viewedObject.SetActive(true);
            viewedObject.layer = cameraLayer;
            viewedObject.transform.localPosition = item.position;
            viewedObject.transform.localRotation = item.rotation;
            viewedObject.transform.localScale = item.scale;
        }
    }

    private GameObject viewedObject;

    public int cameraLayer;
    public float rotateSensitivity = 1;
    public GameObject hotbar;
    public MouseLook mouseLook;

    public void Open()
    {
        mouseLook.enabled = false;
        hotbar.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Close()
    {
        mouseLook.enabled = true;
        hotbar.SetActive(true);
        gameObject.SetActive(false);
    }

    private Vector2 lastMousePosition;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Close();
        }
        else if (Input.GetButton("Fire2"))
        {
            if (!Input.GetButtonDown("Fire2"))
            {
                Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePosition;
                viewedObject.transform.Rotate(Camera.main.transform.up, -mouseDelta.x * rotateSensitivity, Space.World);
                viewedObject.transform.Rotate(Camera.main.transform.right, mouseDelta.y * rotateSensitivity, Space.World);
            }
            lastMousePosition = Input.mousePosition;
        }
    }
}
