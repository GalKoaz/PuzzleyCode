using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class move_text : MonoBehaviour
{
    public Transform door_pos;
    public float speed = 1.5f;
    private bool move = true;
    public string scene_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
         transform.position = Vector3.MoveTowards(transform.position, door_pos.position, speed * Time.deltaTime);
        }
        if (transform.position.Equals(door_pos.position))
        {
            SceneManager.LoadScene(scene_name);
        }
        
    }
    public void change_move()
    {
        move = !move;
    }
}
