using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToRoom : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private string nextSceneName;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                Debug.Log("Loading Next Scene");
                LoadNextSceneInBackground();
            }
        }
    }
    
    void LoadNextSceneInBackground()
    {
        // Use LoadSceneAsync to load the scene in the background
        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
    }
}