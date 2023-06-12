using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image slotIcon;
    [SerializeField] private Text slotAmountLabel;
    [SerializeField] private GameObject slotStackObj;
    [SerializeField] private Text stackSlotLabel;
    
    // NOTE: for now it's only updates the picture
    public void Set(InventoryItem item)
    {
        slotIcon.sprite = item.data.icon;
    }
}
