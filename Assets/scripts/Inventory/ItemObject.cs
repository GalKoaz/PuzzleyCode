using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData item;

    public void OnHandlePickupItem()
    {
        InventorySystem.Instance.Add(item);
        Destroy(gameObject);
    }
}
