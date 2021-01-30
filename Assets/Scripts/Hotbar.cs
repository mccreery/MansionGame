using System;
using System.Collections.Generic;
using UnityEngine;

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
            selectedSlot = value;
            UpdateSlots();
        }
    }

    private GameObject[] slots;
    private List<GameObject> items;

    public void Add(GameObject item, ItemPickup pickup)
    {
        Add(item, pickup.position, pickup.rotation, pickup.scale);
    }

    public void Add(GameObject item, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        item.transform.localPosition = position;
        item.transform.localRotation = rotation;
        item.transform.localScale = scale;

        items.Add(item);
        UpdateSlots();
    }

    public GameObject Remove(int slot)
    {
        GameObject item = slots[slot];
        item.layer = restoreLayer;
        items.RemoveAt(slot);
        UpdateSlots();
        return item;
    }

    public int cameraLayer;
    public int restoreLayer;

    private void UpdateSlots()
    {
        // Create slots
        for (int i = 0; i < numberOfSlots; i++)
        {
            slots[i] = Instantiate(slotPrefab);

            Transform highlight = slots[i].transform.Find("Highlight");
            if (highlight != null)
            {
                highlight.gameObject.SetActive(i == selectedSlot);
            }
        }

        // Fill slots with current items
        // Do this before destroying the old slots to prevent destroying items
        for (int i = 0; i < items.Count; i++)
        {
            Transform item = slots[i].transform.Find("Item");
            if (item != null)
            {
                items[i].layer = cameraLayer;
                items[i].transform.SetParent(item, false);
            }
        }

        // Destroy old slots
        foreach (Transform child in GetChildren(transform))
        {
            Destroy(child.gameObject);
        }

        // Replace old slots with new slots
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].transform.SetParent(transform, false);
        }
    }

    private void Start()
    {
        slots = new GameObject[numberOfSlots];
        items = new List<GameObject>();

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
}
