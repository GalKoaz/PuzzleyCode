using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadAmirLetters : MonoBehaviour
{
    [SerializeField] private GameObject noteCollideGameObject;
    [SerializeField] private KeyCode readButton;

    private bool isNoteSpawned = false;
    private bool _triggered = false;

    private GameObject _uiCanvas;
    private CanvasManger canvasManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiCanvas = GameObject.Find("_GAME_UI");
        canvasManager = _uiCanvas.GetComponent<CanvasManger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_triggered)
        {
            if (IsTrigger())
            {
                ObjectiveManager.Instance.currentObjective.isTriggered = true;
            }
        }

    }


    bool IsTrigger()
    {
        GameObject currRaycastObj = canvasManager.CurrRaycastObj;


        if (!currRaycastObj || !noteCollideGameObject)
        {
            return false;
        }

        if (noteCollideGameObject.GetInstanceID() == currRaycastObj.GetInstanceID() && Input.GetKeyDown(readButton))
        {
            Debug.Log("Triggered!");
            _triggered = true;
            return true;
        }

        return false;
    }
}
