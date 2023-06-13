using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectUSB : MonoBehaviour
{
    [SerializeField] private GameObject usbGameObj;

    private ObjectiveData objectiveData;
    private InventoryItemData usbInventoryItemData;
    private bool _triggered = false;

    private void Start()
    {
        ItemObject itemObject = usbGameObj.GetComponent<ItemObject>();
        if (itemObject)
        {
            usbInventoryItemData = itemObject.item;
        }
        
        ObjectiveObj objectiveObj = gameObject.GetComponent<ObjectiveObj>();
        if (objectiveObj)
        {
            objectiveData = objectiveObj.objectiveData;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_triggered)
        {
            if (IsTrigger())
            {
                objectiveData.isTriggered = true;
            }   
        }
    }

    bool IsTrigger()
    {
        // Check in inventory if the usb is there
        InventoryItem usbInventoryItem = InventorySystem.Instance.Get(usbInventoryItemData);
        if (usbInventoryItem != null)
        {
            // InventorySystem.Instance.Remove(usbInventoryItemData);
            _triggered = true;
            return true;
        }

        return false;
    }
}
