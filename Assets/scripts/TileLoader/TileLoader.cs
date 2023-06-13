using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileLoader : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private string sceneName;
    private Collider player;
    private bool canChangeScene = false;
    private GameObject _uiCanvas;
    private CanvasManger CanvasManger;
    private GameObject gameUICanvas;

    private void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        CanvasManger = _uiCanvas.GetComponent<CanvasManger>();
        gameUICanvas = CanvasManger.GameUI;
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
        if (canChangeScene && Input.GetKeyDown(KeyCode.E))
        {
            LoadNextScene();
            player.gameObject.SetActive(false);
            gameUICanvas.SetActive(false);
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
}