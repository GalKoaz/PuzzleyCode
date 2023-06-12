using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    [SerializeField] private GameObject playerCameraGameObject;
    private Camera _playerCamera;
    
    [Header("Raycast")]
    public float RaycastRange = 3;
    public LayerMask cullLayers;
    public LayerMask interactLayers;

    [Header("Crosshair Textures")]
    public Sprite defaultCrosshair;
    public Sprite interactCrosshair;
    private Sprite default_interactCrosshair;

    [Header("Crosshair")]
    public Image CrosshairUI;
    public int crosshairSize = 5;
    public int interactSize = 10;
    
    
    #region Private Variables
    [HideInInspector] public bool isHeld = false;
    [HideInInspector] public bool inUse;
    [HideInInspector] public Ray playerAim;
    [HideInInspector] public GameObject RaycastObject;
    
    private GameObject _uiCanvas;
    private CanvasManger canvasManager;
    private GameObject LastRaycastObject;

    private int default_interactSize;
    private int default_crosshairSize;

    private string bp_Use;
    private string bp_Pickup;
    private bool UsePressed;

    private bool isPressed;
    private bool isCorrectLayer;
    #endregion

    private void Awake()
    {
        _playerCamera = playerCameraGameObject.GetComponent<Camera>();
        
        // Set canvas manager
        _uiCanvas = GameObject.Find("_GAME_UI");
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = playerCameraGameObject.GetComponent<Camera>();
        
        // Set canvas manager
        _uiCanvas = GameObject.Find("_GAME_UI");
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("UPDATE RAYCAST!!!");

        Ray playerAimRay = _playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(playerAimRay.origin, playerAimRay.direction * RaycastRange, Color.red, cullLayers);
        if (Physics.Raycast(playerAimRay.origin, playerAimRay.direction * RaycastRange, out var hit))
        {
            RaycastObject = hit.collider.gameObject;
            
            // Object is clickable - change cursor accordingly
            InteractionPanelConfig interactionPanelConfig = RaycastObject.GetComponent<InteractionPanelConfig>();
            if (interactionPanelConfig)
            {
                //Debug.Log("IS CLICKABLE!");
                CrosshairChange(true);
                interactionPanelConfig.Active(true);
                interactionPanelConfig.SetVisualInfo();
                canvasManager.IsRaycastInteract = true;
                canvasManager.CurrRaycastObj = RaycastObject;
            }
            else
            {
                //Debug.Log("ISN'T CLICKABL0E!");
                CrosshairChange(false);
                canvasManager.SetActive(false);
                canvasManager.IsRaycastInteract = false;
            }

        }

        
        
    }
    
    void CrosshairChange(bool useTexture)
    {
        if (useTexture && CrosshairUI.sprite != interactCrosshair)
        {
            CrosshairUI.sprite = interactCrosshair;
            CrosshairUI.GetComponent<RectTransform>().sizeDelta = new Vector2(interactSize, interactSize);
        }
        else if (!useTexture && CrosshairUI.sprite != defaultCrosshair)
        {
            CrosshairUI.sprite = defaultCrosshair;
            CrosshairUI.GetComponent<RectTransform>().sizeDelta = new Vector2(crosshairSize, crosshairSize);
        }

        CrosshairUI.DisableSpriteOptimizations();
    }
    
    private void ResetCrosshair()
    {
        crosshairSize = default_crosshairSize;
        interactSize = default_interactSize;
        interactCrosshair = default_interactCrosshair;
    }

    public void CrosshairVisible(bool state)
    {
        switch (state)
        {
            case true:
                CrosshairUI.enabled = true;
                break;
            case false:
                CrosshairUI.enabled = false;
                break;
        }
    }
}
