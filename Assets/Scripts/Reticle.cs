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
    private RectTransform rectTransform;

    private void Start()
    {
        image = GetComponent<Image>();
        // Copy material so it doesn't update asset
        image.material = new Material(image.material);

        rectTransform = GetComponent<RectTransform>();
    }

    public Hotbar hotbar;
    public Inspector inspector;

    public bool Hit { get; private set; }

    private RaycastHit raycastHit;
    public RaycastHit RaycastHit => raycastHit;

    public float reach = 3;

    private const string InteractableTag = "Interactable";

    private void Update()
    {
        IInteractable pointOfInterest;
        int inventoryMask = 1 << inspector.cameraLayer;

        if (inspector.Open)
        {
            transform.position = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, float.PositiveInfinity, inventoryMask);

            if (!Input.GetButton("Fire2"))
            {
                pointOfInterest = GetInspectorInteraction(hits);
            }
            else
            {
                pointOfInterest = null;
            }
        }
        else
        {
            rectTransform.anchoredPosition = Vector2.zero;

            Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            Hit = Physics.Raycast(ray, out raycastHit, float.PositiveInfinity, ~inventoryMask);

            if (Hit && raycastHit.distance < reach && raycastHit.transform.CompareTag(InteractableTag))
            {
                pointOfInterest = raycastHit.transform.GetComponent<IInteractable>();
            }
            else
            {
                pointOfInterest = null;
            }
        }

        radius = Mathf.SmoothDamp(radius, pointOfInterest != null ? maxRadius : 0, ref radiusVelocity, smoothTime);
        image.material.SetFloat(radiusProperty, radius);

        if (Input.GetButtonDown("Fire1"))
        {
            ItemPickup heldItem = hotbar[hotbar.SelectedSlot];

            if (pointOfInterest != null)
            {
                pointOfInterest.Interact(hotbar);
            }
            else if (!inspector.Open && heldItem != null)
            {
                hotbar.Inspect();
            }
        }
    }

    private IInteractable GetInspectorInteraction(RaycastHit[] hits)
    {
        if (hits.Length == 0)
        {
            return new Interactable(hotbar => inspector.Open = false);
        }

        foreach (RaycastHit hit in hits)
        {
            if (hit.transform.CompareTag(InteractableTag))
            {
                IInteractable interactable = hit.transform.GetComponent<IInteractable>();

                // Ignore world pickups
                if (!(interactable is ItemPickup))
                {
                    return interactable;
                }
            }
        }
        return null;
    }
}
