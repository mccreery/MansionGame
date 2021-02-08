using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Reticle : MonoBehaviour
{
    [Range(0, 1)]
    public float maxRadius;

    [Min(0)]
    public float smoothTime;

    public string radiusProperty;

    private float radius;
    private float radiusVelocity;

    private Image image;
    private bool interested;

    private void Start()
    {
        image = GetComponent<Image>();
        // Copy material so it doesn't update asset
        image.material = new Material(image.material);
    }

    public Hotbar hotbar;

    private RaycastHit raycastHit;
    public RaycastHit Raycast => raycastHit;

    public bool Hit { get; private set; }

    public float reach = 3;

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));

        Hit = Physics.Raycast(ray, out raycastHit);
        interested = Hit && raycastHit.distance < reach && raycastHit.transform.gameObject.CompareTag("Interactable");

        radius = Mathf.SmoothDamp(radius, interested ? maxRadius : 0, ref radiusVelocity, smoothTime);
        image.material.SetFloat(radiusProperty, radius);

        if (Input.GetButtonDown("Fire1"))
        {
            ItemPickup heldItem = hotbar[hotbar.SelectedSlot];

            if (interested)
            {
                IInteractable interactable = raycastHit.transform.GetComponent<IInteractable>();
                hotbar.Replace(hotbar.SelectedSlot, interactable.Interact(heldItem));
            }
            else if (heldItem != null)
            {
                hotbar.Inspect();
            }
        }
    }
}
