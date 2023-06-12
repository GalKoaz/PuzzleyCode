using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_door: MonoBehaviour
{
    public Transform door_pos;
    public float speed = 1.5f;
    private bool move= false;
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
    }

    public void change_move()
    {
        move = !move;
    }
}
