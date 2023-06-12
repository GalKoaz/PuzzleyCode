using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private static InventorySystem _current; // Singleton instance
    
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; private set; }
    
    // Event to notify subscribers when the inventory changes
    public event Action OnInventoryChangedEvent;
    
    private void Awake()
    {
        // Check if an instance already exists and destroy it
        if (_current != null)
        {
            Destroy(gameObject);
            return;
        }
        // Set the current instance as the singleton instance
        _current = this;
        
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        
        // Keep the inventory system object persistent across scenes
        DontDestroyOnLoad(gameObject);
    }
    
    public static InventorySystem Instance
    {
        get { return _current; }
    }


    public InventoryItem Get(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }

        return null;
    }
    
    
    public void Add(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        
        // Trigger the inventory changed event
        OnInventoryChangedEvent?.Invoke();
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        
        // Trigger the inventory changed event
        OnInventoryChangedEvent?.Invoke();
    }

    public void DebugPrintArray()
    {
        foreach (InventoryItem inventoryItem in _current.inventory)
        {
            Debug.Log("ITEM: " + inventoryItem.data);
        }
    }

    private void Update()
    {
        // DebugPrintArray();
    }
}
