using UnityEngine;

public class Inspector : MonoBehaviour
{
    private ItemPickup item;

    // Used to return the item to the hotbar when finished inspecting
    private Transform previousParent;

    public int cameraLayer;
    public float rotateSensitivity = 1;
    public GameObject hotbar;
    public MouseLook mouseLook;

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
    }

    public void Close()
    {
        mouseLook.enabled = true;
        Cursor.visible = true;
        hotbar.SetActive(true);
        gameObject.SetActive(false);

        item.transform.SetParent(previousParent, false);
        item.InventoryTransformData.CopyTo(item.transform, false);
    }

    public bool IsOpen => gameObject.activeSelf;

    private Vector2 lastMousePosition;

    private void Update()
    {
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
