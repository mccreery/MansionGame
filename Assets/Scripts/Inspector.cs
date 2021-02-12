using UnityEngine;

public class Inspector : MonoBehaviour
{
    private ItemPickup item;
    public ItemPickup Item => item;

    // Used to return the item to the hotbar when finished inspecting
    private Transform previousParent;

    public int cameraLayer;

    public float rotateSensitivity = 1;
    public float zoomSensitivity = 1;

    public GameObject hotbar;
    public MouseLook mouseLook;

    private Vector3 restoreScale;

    public void Inspect(ItemPickup item)
    {
        mouseLook.enabled = false;
        Cursor.visible = false;
        hotbar.SetActive(false);
        gameObject.SetActive(true);

        this.item = item;
        previousParent = item.transform.parent;
        item.transform.SetParent(transform, false);
        item.InspectorTransformData.CopyTo(item.transform, false);

        restoreScale = gameObject.transform.localScale;
        scale = 1;
    }

    public void Close()
    {
        mouseLook.enabled = true;
        Cursor.visible = true;
        hotbar.SetActive(true);
        gameObject.SetActive(false);

        item.transform.SetParent(previousParent, false);
        item.InventoryTransformData.CopyTo(item.transform, false);

        gameObject.transform.localScale = restoreScale;
    }

    public bool IsOpen => gameObject.activeSelf;

    private Vector2 lastMousePosition;

    private float scale;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            scale = Mathf.Clamp(scale + Input.mouseScrollDelta.y * zoomSensitivity, minScale, maxScale);
            gameObject.transform.localScale = restoreScale * scale;
        }

        if (Input.GetButton("Fire2"))
        {
            if (!Input.GetButtonDown("Fire2"))
            {
                Vector2 mouseDelta = (Vector2)Input.mousePosition - lastMousePosition;
                item.transform.Rotate(Camera.main.transform.up, -mouseDelta.x * rotateSensitivity, Space.World);
                item.transform.Rotate(Camera.main.transform.right, mouseDelta.y * rotateSensitivity, Space.World);
            }
            lastMousePosition = Input.mousePosition;
        }
    }
}
