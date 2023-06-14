using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ReadGalNote : MonoBehaviour
{
    [SerializeField] private GameObject noteGameObject;
    [SerializeField] private GameObject noteCollideGameObject;
    [SerializeField] private ObjectiveData plugUsbObjData;
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
        if (!isNoteSpawned && plugUsbObjData.isCompleted)
        {
            // Spawn the letter to read the secret code
            noteGameObject.SetActive(true);
            isNoteSpawned = true;
        }

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