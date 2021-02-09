using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private GameObject[] slots;
    private ItemPickup[] items;

    [SerializeField]
    [Min(0)]
    private int numberOfSlots = 5;

    public int NumberOfSlots
    {
        get => numberOfSlots;
        set
        {
            numberOfSlots = value;
            Clear();
        }
    }

    public int cameraLayer;

    private void Awake() => Clear();

    private void Clear()
    {
        slots = new GameObject[numberOfSlots];
        items = new ItemPickup[numberOfSlots];
        selectedSlot = 0;
        UpdateSlots();
    }

    public GameObject slotPrefab;

    public Inspector inspector;

    private int selectedSlot;
    public int SelectedSlot
    {
        get => selectedSlot;
        set
        {
            selectedSlot = value;
            UpdateSlots();
        }
    }

    public ItemPickup this[int slot] => items[slot];

    public ItemPickup SelectedItem => items[selectedSlot];

    public int Add(ItemPickup pickup)
    {
        int nextSlot = Array.IndexOf(items, null);

        if (nextSlot != -1)
        {
            Replace(nextSlot, pickup, false);
        }
        return nextSlot;
    }

    public ItemPickup Remove(int slot, bool returnItem)
    {
        return Replace(slot, null, returnItem);
    }

    public ItemPickup Replace(int slot, ItemPickup newItem, bool returnItem)
    {
        ItemPickup oldItem = items[slot];

        if (oldItem != newItem)
        {
            items[slot] = newItem;
            UpdateSlots();
        }

        if (newItem != null)
        {
            newItem.gameObject.SetActive(false);
        }
        if (oldItem != null && returnItem)
        {
            oldItem.gameObject.SetActive(true);
        }
        return oldItem;
    }

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

            if (items[i] != null)
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

                    pickup.inventoryTransformData.CopyTo(itemCopy.transform, false);
                }
            }
        }
    }

    public void Inspect()
    {
        inspector.Item = this[selectedSlot];
        inspector.Open = true;
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

        if (Input.GetButtonDown("Return Item"))
        {
            Remove(SelectedSlot, true);
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
