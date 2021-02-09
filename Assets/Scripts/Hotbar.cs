using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [Min(0)]
    public int numberOfSlots = 5;

    public int cameraLayer;
    public GameObject slotPrefab;
    public Inspector inspector;

    [Min(0)]
    public float scrollCooldown;

    private GameObject[] slots;
    private ItemPickup[] items;

    private void Start()
    {
        slots = new GameObject[numberOfSlots];
        items = new ItemPickup[numberOfSlots];
        selectedSlot = 0;

        CreateSlots();
        UpdateHighlights();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(slotPrefab, transform);

            // Display slot number
            Text text = slots[i].GetComponentInChildren<Text>();
            if (text != null)
            {
                text.text = (i + 1).ToString();
            }
        }
    }

    private int selectedSlot;
    public int SelectedSlot
    {
        get => selectedSlot;
        set
        {
            selectedSlot = value;
            UpdateHighlights();
        }
    }

    private void UpdateHighlights()
    {
        // Show highlight rect around selected slot only
        for (int i = 0; i < slots.Length; i++)
        {
            Transform highlight = slots[i].transform.Find("Highlight");
            if (highlight != null)
            {
                highlight.gameObject.SetActive(i == selectedSlot);
            }
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
        for (int i = 0; i < slots.Length; i++)
        {
            // Instantiate a copy of the pickup
            Transform parentTransform = slots[i].transform.Find("Item");

            if (parentTransform != null)
            {
                // Destroy previous copy
                foreach (Transform childTransform in parentTransform)
                {
                    Destroy(childTransform.gameObject);
                }

                if (items[i] != null)
                {
                    // Create new copy
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

    public static void SetLayerRecursively(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;

        foreach (Transform child in gameObject.transform) {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}
