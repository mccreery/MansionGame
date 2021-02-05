using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    public int numberOfSlots = 5;
    public GameObject slotPrefab;

    private int selectedSlot;
    public int SelectedSlot
    {
        get => selectedSlot;
        set
        {
            selectedSlot = Mathf.Clamp(value, 0, numberOfSlots);
            UpdateSlots();
        }
    }

    private GameObject[] slots;
    private List<ItemPickup> items;

    public ItemPickup this[int slotIndex]
    {
        get
        {
            if (slotIndex >= 0 && slotIndex < items.Count)
            {
                return items[slotIndex];
            }
            else
            {
                return null;
            }
        }
    }

    public void Add(ItemPickup pickup)
    {
        items.Add(pickup);
        pickup.gameObject.SetActive(false);
        UpdateSlots();
    }

    public ItemPickup Remove(int slot, bool returnItem = true)
    {
        ItemPickup pickup = items[slot];
        items.RemoveAt(slot);
        pickup.gameObject.SetActive(returnItem);
        UpdateSlots();
        return pickup;
    }

    public int cameraLayer;

    private void UpdateSlots()
    {
        // Destroy old slots
        foreach (Transform child in GetChildren(transform))
        {
            Destroy(child.gameObject);
        }

        // Create new slots
        for (int i = 0; i < numberOfSlots; i++)
        {
            // Create a new slot
            slots[i] = Instantiate(slotPrefab, transform);

            // Set the slot number
            Transform number = slots[i].transform.Find("Number");
            if (number != null)
            {
                number.gameObject.GetComponent<Text>().text = (i + 1).ToString();
            }

            // Select the slot if current
            Transform highlight = slots[i].transform.Find("Highlight");
            if (highlight != null)
            {
                highlight.gameObject.SetActive(i == selectedSlot);
            }

            if (i < items.Count)
            {
                // Instantiate a copy of the pickup
                Transform parentTransform = slots[i].transform.Find("Item");
                if (parentTransform != null)
                {
                    ItemPickup pickup = items[i];
                    GameObject itemCopy = Instantiate(pickup.gameObject, parentTransform);
                    itemCopy.SetActive(true);

                    // Ensure visible to camera
                    SetLayerRecursively(itemCopy, cameraLayer);

                    itemCopy.transform.localPosition = pickup.position;
                    itemCopy.transform.localRotation = pickup.rotation;
                    itemCopy.transform.localScale = pickup.scale;
                }
            }
        }
    }

    private void Start()
    {
        slots = new GameObject[numberOfSlots];
        items = new List<ItemPickup>();

        SelectedSlot = 0;
        UpdateSlots();
    }

    private float lastScrollTime = Mathf.NegativeInfinity;

    [Min(0)]
    public float scrollCooldown;

    private void Update()
    {
        for (int slot = 0; slot < numberOfSlots; slot++)
        {
            KeyCode keyCode = KeyCode.Alpha1 + slot;

            if (Input.GetKeyDown(keyCode))
            {
                SelectedSlot = slot;
                return;
            }
        }

        if (Time.time - lastScrollTime > scrollCooldown)
        {
            int scroll = -Math.Sign(Input.mouseScrollDelta.y);
            if (scroll != 0)
            {
                lastScrollTime -= Time.time;
                SelectedSlot = FloorMod(SelectedSlot + scroll, numberOfSlots);
            }
        }

        if (Input.GetButtonDown("Return Item") && SelectedSlot < items.Count)
        {
            Remove(SelectedSlot);
        }
    }

    private static int FloorMod(int a, int b)
    {
        return a - b * (int)Mathf.Floor((float)a / b);
    }

    private static IEnumerable<Transform> GetChildren(Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            yield return parent.GetChild(i);
        }
    }

    public static void SetLayerRecursively(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;

        foreach (Transform child in GetChildren(gameObject.transform)) {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
