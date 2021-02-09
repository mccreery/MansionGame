using System;
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

            if (newItem != null)
            {
                newItem.transform.SetParent(slots[slot].transform, false);
                newItem.InventoryTransformData.CopyTo(newItem.transform, false);
                SetLayerRecursively(newItem.gameObject, cameraLayer);
            }

            if (oldItem != null)
            {
                if (returnItem)
                {
                    oldItem.WorldTransformData.CopyTo(oldItem.transform, true);
                    SetLayerRecursively(oldItem.gameObject, 0);
                }
                else
                {
                    Destroy(oldItem.gameObject);
                }
            }
        }
        return oldItem;
    }

    public void Inspect()
    {
        inspector.Inspect(SelectedItem);
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
