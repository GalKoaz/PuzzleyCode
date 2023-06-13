using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileLoader : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private string sceneName;
    [SerializeField] private InventoryItemData usbInventoryItem;
    [SerializeField] private GameObject plugUsbObjectiveObj;
    
    private Collider player;
    private bool canChangeScene = false;
    private bool isSceneLoaded = false;
    private GameObject _uiCanvas;
    private CanvasManger CanvasManger;
    private GameObject gameUICanvas;
    private InventoryItem inventoryItem;
    private ObjectiveData plugUsbObjectiveData;

    
    private void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        CanvasManger = _uiCanvas.GetComponent<CanvasManger>();
        gameUICanvas = CanvasManger.GameUI;

        ObjectiveObj objectiveObj = plugUsbObjectiveObj.GetComponent<ObjectiveObj>();
        if (objectiveObj)
        {
            plugUsbObjectiveData = objectiveObj.objectiveData;    
        }
    }

    // This function is called when another collider enters this object's collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player (assuming the player has a tag "Player")
        if (other.gameObject.CompareTag("Player"))
        {
            canChangeScene = true;
            player = other;
        }
    }

    // This function is called when another collider exits this object's collider
    void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.gameObject.CompareTag("Player"))
        {
            canChangeScene = false;
        }
    }

    void Update()
    {
        // Only if user has the USB
        if (HasUSB())
        {
            if (!isSceneLoaded && canChangeScene && Input.GetKeyDown(KeyCode.E))
            {
                isSceneLoaded = true;
                LoadNextScene();
                player.gameObject.SetActive(false);
                gameUICanvas.SetActive(false);
                plugUsbObjectiveData.isTriggered = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canChangeScene)
        {
            UnloadCurrentScene();
            player.gameObject.SetActive(true);
            gameUICanvas.SetActive(true);
        }
    }

    void LoadNextScene()
    { 
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive); 
        //SceneManager.LoadScene(scene);
    }
    
    void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    bool HasUSB()
    {
        inventoryItem = InventorySystem.Instance.Get(usbInventoryItem);
        return inventoryItem != null;
    }
}