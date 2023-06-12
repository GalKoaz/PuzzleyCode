using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickMoveScene : MonoBehaviour
{
    public string next_scene;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDown()
    {
        SceneManager.LoadScene(next_scene);
    }
    public void change_number()
    {
        this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "" + next_scene;
        return;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
