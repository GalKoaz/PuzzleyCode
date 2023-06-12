using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Given class allows accessing UI Canvas objects in order to change them (or their values)*/
public class CanvasManger : MonoBehaviour
{
    [SerializeField] private GameObject _interactionInfo;
    [SerializeField] private GameObject _control1;
    [SerializeField] private GameObject _control2;
    [SerializeField] private GameObject noteInteractionPanel;
    [SerializeField] private GameObject playerGameObj;
    [SerializeField] private GameObject inventoryPanel;
    
    /*******  Raycast SECTION ********/
    // Current raycast hit object
    private GameObject _currRaycastObj;
    
    public bool IsRaycastInteract { get; set; } = false;  // Indicates whether raycast currently collides on any of the interactable props
    public GameObject CurrRaycastObj
    {
        get { return _currRaycastObj; }
        set { _currRaycastObj = value; }
    }

    // Getter and setter for _interactionInfo
    public GameObject InteractionInfo
    {
        get { return _interactionInfo; }
        set { _interactionInfo = value; }
    }

    // Getter and setter for _control1
    public GameObject Control1
    {
        get { return _control1; }
        set { _control1 = value; }
    }

    // Getter and setter for _control2
    public GameObject Control2
    {
        get { return _control2; }
        set { _control2 = value; }
    }


    public void SetActive(bool activeFlag)
    {
        if(_interactionInfo) _interactionInfo.SetActive(activeFlag);
        if(_control1) _control1.SetActive(activeFlag);
        if(_control2) _control2.SetActive(activeFlag);
    }
    
    
    /*******  NOTE INTERACTION SECTION ********/
    // Getter and setter for noteInteractionPanel
    public GameObject NoteInteractionPanel
    {
        get { return noteInteractionPanel; }
        set { noteInteractionPanel = value; }
    }
    
    /*******  Player SECTION ********/
    // Getter and setter for noteInteractionPanel
    public GameObject PlayerGameObj
    {
        get { return playerGameObj; }
        set { playerGameObj = value; }
    }

    public void DisablePlayer(bool disableFlag)
    {
        playerGameObj.GetComponent<SimpleSampleCharacterControl>().enabled = !disableFlag;
        playerGameObj.GetComponent<MouseController>().enabled = !disableFlag;
    }
    
    
    /*******  Inventory SECTION ********/
    public GameObject InventoryPanel
    {
        get { return inventoryPanel; }
        set { inventoryPanel = value; }
    }
}
