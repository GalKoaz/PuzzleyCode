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
    
    void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
        playerObj = canvasManager.PlayerGameObj;
        StartCoroutine(UnloadScenesAfterDelay());
    }

    IEnumerator UnloadScenesAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.UnloadSceneAsync(NextScenes);
        playerObj.gameObject.SetActive(true);
    }
}