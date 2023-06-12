using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPanelConfig : MonoBehaviour
{
    [Header("component's interaction title (optional title)")]
    [SerializeField] private string title;

    [Header("First action configuration (optional control)")] 
    [SerializeField] private Sprite firstActionButton;
    [SerializeField] private string firstActionTitle;
    
    [Header("Second action configuration (optional control)")]
    [SerializeField] private Sprite secActionButton;
    [SerializeField] private string secActionTitle;


    private GameObject _uiCanvas;
    private CanvasManger canvasManager;
    private GameObject _interactionInfo;
    private GameObject _control1;
    private GameObject _control2;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        Debug.Log("DEBUG: " + _uiCanvas);
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
        
        _interactionInfo = canvasManager.InteractionInfo;
        _control1 = canvasManager.Control1;
        _control2 = canvasManager.Control2;
        
        // Debug.Log((_interactionInfo == null) + " " + (_control1 == null) + " " + (_control2 == null));
    }

    public void SetVisualInfo()
    {
        // Set given values
        if (!String.IsNullOrEmpty(title))
        {
            _interactionInfo.GetComponentInChildren<Text>().text = title;
        }
        else
        {
            _interactionInfo.SetActive(false);
        }

        if (!String.IsNullOrEmpty(firstActionTitle))
        {
            _control1.GetComponentInChildren<Image>().sprite = firstActionButton;
            _control1.GetComponentInChildren<Text>().text = firstActionTitle;
        }
        else
        {
            _control1.SetActive(false);
        }

        
        if (!String.IsNullOrEmpty(secActionTitle))
        {
            _control2.GetComponentInChildren<Image>().sprite = secActionButton;
            _control2.GetComponentInChildren<Text>().text = secActionTitle;  
        }
        else
        {
            _control2.SetActive(false);
        }
    }

    public void Active(bool activeFlag)
    {
        Debug.Log((_interactionInfo == null) + " " + (_control1 == null)+ " " + (_control2 == null));
        if(_interactionInfo) _interactionInfo.SetActive(activeFlag);
        if(_control1) _control1.SetActive(activeFlag);
        if(_control2) _control2.SetActive(activeFlag);
    }
}
