using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInteraction : MonoBehaviour
{
    [Tooltip("Open the inventory button")] [SerializeField]
    private KeyCode interactInputAction;
    
    private GameObject _uiCanvas;
    private CanvasManger canvasManager;
    private GameObject inventoryPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        Debug.Log("DEBUG: " + _uiCanvas);
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
        
        inventoryPanel = canvasManager.InventoryPanel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(interactInputAction))
        {
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
}
