using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NoteInteract : MonoBehaviour
{
    [Header("Input")]
    [Tooltip("exit inspecting the letter")]
    [SerializeField] private KeyCode exitInputAction;
    [Tooltip("Enter inspecting the letter in UI")]
    [SerializeField] private KeyCode interactInputAction;

    [Header("Text UI")]
    [SerializeField] [TextArea] private string noteDateText;
    [SerializeField] [TextArea] private string noteTimeText;
    [SerializeField] [TextArea] private string noteBodyText;
    [Tooltip("Optional image in bottom of the note")]
    [SerializeField] private Sprite noteImage;
    
    
    private GameObject _uiCanvas;
    private CanvasManger canvasManager;
    
    private GameObject playerGameObj;
    
    // Note object and its text children
    private GameObject noteGameObject;
    private Text noteDateTextAreaUI;
    private Text noteTimeTextAreaUI;
    private Text noteBodyTextAreaUI;
    private Image noteImageAreaUI;  // Optional image to the note
    
    private bool isOpen = false;

    private void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        Debug.Log("DEBUG: " + _uiCanvas);
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();

        playerGameObj = canvasManager.PlayerGameObj;
        
        noteGameObject = canvasManager.NoteInteractionPanel;
        noteDateTextAreaUI = noteGameObject.transform.Find("PaperReadDateText").GetComponent<Text>();
        noteTimeTextAreaUI = noteGameObject.transform.Find("PaperReadTimeText").GetComponent<Text>();
        noteBodyTextAreaUI = noteGameObject.transform.Find("PaperReadBodyText").GetComponent<Text>();
        noteImageAreaUI = noteGameObject.transform.Find("PaperReadImg").GetComponent<Image>();
    }


    void ShowNote()
    {
        noteDateTextAreaUI.text = noteDateText;
        noteTimeTextAreaUI.text = noteTimeText;
        noteBodyTextAreaUI.text = noteBodyText;

        // If there is image in inspector
        if (noteImageAreaUI)
        {
            noteImageAreaUI.gameObject.SetActive(true);
            noteImageAreaUI.sprite = noteImage;
        }
        else
        {
            noteImageAreaUI.gameObject.SetActive(false);
        }        
        
        noteGameObject.SetActive(true);
        canvasManager.DisablePlayer(true);
        isOpen = true;
    }

    void DisableNote()
    {
        noteGameObject.SetActive(false);
        canvasManager.DisablePlayer(false);
        isOpen = false;
    }

    private void Update()
    {
        // DEBUG
        // if (canvasManager.CurrRaycastObj)
        // {
        //     Debug.Log("Interact raycast: "
        //               + canvasManager.IsRaycastInteract
        //               + ", Interact input action: "
        //               + Input.GetKeyDown(interactInputAction)
        //               + " " + (GetInstanceID() == canvasManager.CurrRaycastObj.GetInstanceID()));   
        // }

        // Raycast is on object and interaction key is down
        if (canvasManager.IsRaycastInteract 
            && Input.GetKeyDown(interactInputAction) 
            && this.gameObject.GetInstanceID() == canvasManager.CurrRaycastObj.GetInstanceID())
        {
            ShowNote();
        }
        
        // If note is opened and player wants to exit the interaction
        if (isOpen)
        {
            if (Input.GetKeyDown(exitInputAction))
            {
                DisableNote();
            }   
        }
    }
    
}
