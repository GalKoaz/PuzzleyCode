using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class you_win : MonoBehaviour
{
    public int sleep_time;
    public string scene_levels;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter_not_that_waiter_just_waiter());
    }
    IEnumerator waiter_not_that_waiter_just_waiter()
    {
        yield return new WaitForSeconds(sleep_time);
        //my code here after 3 seconds
        SceneManager.LoadScene(scene_levels);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
