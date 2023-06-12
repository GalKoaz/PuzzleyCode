using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : MonoBehaviour
{
    [Tooltip("Pickup interaction key")] [SerializeField]
    private KeyCode interactInputAction;
    
    private GameObject _uiCanvas;
    private CanvasManger canvasManager;


    // Start is called before the first frame update
    void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        Debug.Log("DEBUG: " + _uiCanvas);
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasManager.IsRaycastInteract 
            && Input.GetKeyDown(interactInputAction) 
            && this.gameObject.GetInstanceID() == canvasManager.CurrRaycastObj.GetInstanceID())
        {
            if (TryGetComponent(out ItemObject itemObject))
            {
                itemObject.OnHandlePickupItem();               
            }
        }
    }
}