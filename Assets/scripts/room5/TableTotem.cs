using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TableTotem : MonoBehaviour
{
    [SerializeField] private InventoryItemData totemInventoryItemData;
    [SerializeField] private LightsOutPuzzle lightsOutPuzzle;
    [SerializeField] private KeyCode putButton;

    [Header("Totem")]
    [SerializeField] private Vector3 totemTablePos;
    [SerializeField] private Vector3 totemRotation;
    
    [Header("Card")]
    [SerializeField] private Vector3 cardTablePos;
    [SerializeField] private Quaternion cardRotation;
    
    [Header("Prefabs")]
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject totemPrefab;
    
    private bool _put = false;
    private bool enabledInteract = false;
    private InteractionPanelConfig _interactionPanelConfig;
    
    private GameObject _uiCanvas;
    private CanvasManger canvasManger;
    private InteractManager _interactManager;
    
    private void Start()
    {
        _interactionPanelConfig = gameObject.GetComponent<InteractionPanelConfig>();
        _uiCanvas = GameObject.Find("_GAME_UI");
        canvasManger = _uiCanvas.GetComponent<CanvasManger>();
        _interactManager = canvasManger.PlayerGameObj.GetComponent<InteractManager>();
        _interactionPanelConfig.Disable = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if can put totem and only then enable interacting with the table
        if (!enabledInteract && CanPutTotem())
        {
            _interactionPanelConfig.Disable = false;  // Enable interaction panel
            enabledInteract = true;
        }
        
        // Check if raycast and pressed put button on the desktop
        if (!_put && Input.GetKeyDown(putButton)
                  && canvasManger.CurrRaycastObj == gameObject)
        {
            PutTotemOnTable();
        }
    }

    // Method checks if the condition to put the totem on the table are satisfied.
    // CONDITIONS:
    //             1. Player picked up the totem.
    //             2. Player solved the puzzle lightsOut.
    bool CanPutTotem()
    {
        InventoryItem totemInventoryItem = InventorySystem.Instance.Get(totemInventoryItemData);
        if (lightsOutPuzzle.Solved && totemInventoryItem != null)
        {
            return true;
        }
        return false;
    }


    void PutTotemOnTable()
    {
        GameObject cardSpawnedGameObj = Instantiate(cardPrefab, cardTablePos, cardRotation);
        GameObject totemSpawnedGameObj = Instantiate(totemPrefab, totemTablePos, Quaternion.Euler(totemRotation));


        InteractionPanelConfig interactionTotemPanelConfig = totemSpawnedGameObj.GetComponent<InteractionPanelConfig>();
        InteractionPanelConfig interactionGameObjectPanelConfig = gameObject.GetComponent<InteractionPanelConfig>();
        
        interactionTotemPanelConfig.Disable = true;
        interactionTotemPanelConfig.Active(false);
        interactionGameObjectPanelConfig.Disable = true;
        interactionGameObjectPanelConfig.Active(false);
        _interactManager.CrosshairChange(false);  // default crosshair

        gameObject.GetComponent<Outline>().enabled = false;
        InventorySystem.Instance.Remove(totemInventoryItemData);  // remove from inventory
        _put = true;
    }
}
