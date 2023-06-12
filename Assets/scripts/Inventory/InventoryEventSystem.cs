using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEventSystem : MonoBehaviour
{
    [Tooltip("Slot prefab to instantiate a new item in inventory")]
    [SerializeField] private GameObject inventorySlotPrefab;
    
    private void Start()
    {
        InventorySystem.Instance.OnInventoryChangedEvent += OnUpdateInventory;
    }

    void OnUpdateInventory()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        DrawInventory();
    }

    public void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.Instance.inventory)
        {
            AddInventorySlot(item);
        }
    }

    void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(inventorySlotPrefab);
        obj.transform.SetParent(transform, false);

        InventoryItemSlotUI objSlot = obj.GetComponent<InventoryItemSlotUI>();
        if (objSlot)
        {
            objSlot.Set(item);
        }
    }
}
