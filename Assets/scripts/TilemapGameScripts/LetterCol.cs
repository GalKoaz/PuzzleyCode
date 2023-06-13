using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LetterCol : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] public string NextScene;
    [SerializeField] public string unload;
    [SerializeField] private TextMeshProUGUI letter;
    private bool sceneLoaded = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player && !sceneLoaded)
        {
            Debug.Log("Player touches Letter trigger");
            gameObject.SetActive(false);
            letter.color = Color.green;
            SceneManager.LoadSceneAsync(NextScene, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(unload);
            sceneLoaded = true;
        }
    }
}
