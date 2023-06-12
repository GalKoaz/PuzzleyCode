using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class load_room1 : MonoBehaviour
{
    public string nextroom;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(nextroom, LoadSceneMode.Additive);
    }

}
