using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTotem : MonoBehaviour
{
    [SerializeField] private GameObject totemGameObj;
    [SerializeField] private Outline tableOutline;
    
    private ObjectiveData objectiveData;
    private InventoryItemData usbInventoryItemData;
    private bool _triggered = false;

    private void Start()
    {
        ItemObject itemObject = totemGameObj.GetComponent<ItemObject>();
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
                tableOutline.enabled = true;
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
