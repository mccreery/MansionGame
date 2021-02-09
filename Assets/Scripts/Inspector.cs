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
            Hotbar.SetLayerRecursively(viewedObject, cameraLayer);

            if (item.overrideForInspector)
            {
                viewedObject.transform.localPosition = item.inspectorPosition;
                viewedObject.transform.localRotation = item.inspectorRotation;
                viewedObject.transform.localScale = item.inspectorScale;
            }
            else
            {
                viewedObject.transform.localPosition = item.position;
                viewedObject.transform.localRotation = item.rotation;
                viewedObject.transform.localScale = item.scale;
            }
        }
    }

    private GameObject viewedObject;

    public int cameraLayer;
    public float rotateSensitivity = 1;
    public GameObject hotbar;
    public MouseLook mouseLook;

    public bool Open
    {
        get => gameObject.activeSelf;
        set
        {
            mouseLook.enabled = !value;
            hotbar.SetActive(!value);
            gameObject.SetActive(value);
        }
    }

    private Vector2 lastMousePosition;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Open = false;
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
