using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] public float delay = 10f;
    [SerializeField] public string NextScenes;
    private GameObject _uiCanvas;
    private CanvasManger canvasManager;
    private GameObject playerObj;
    private GameObject gameUICanvas;

    void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
        playerObj = canvasManager.PlayerGameObj;
        gameUICanvas = canvasManager.GameUI;
        StartCoroutine(UnloadScenesAfterDelay());
    }


    IEnumerator UnloadScenesAfterDelay()
    {
        Debug.Log("ENTER TO THE END script");
        yield return new WaitForSeconds(delay);
        Debug.Log("done DELAY !!");
        SceneManager.UnloadSceneAsync(NextScenes);
        playerObj.gameObject.SetActive(true);
        gameUICanvas.SetActive(true);
    }
}