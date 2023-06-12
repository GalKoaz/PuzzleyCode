using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class buttonui : MonoBehaviour
{
    public GameObject text_moves;
    private bool pressed = false;
    private bool pressed2 = false;
    public float orginal_speed = 1.5f; 
    public float speed_add = 1;

    public void fasterbutton()
    {
        if (pressed)
        {
            text_moves.GetComponent<move_text>().speed = orginal_speed;
        }
        else
        {
            text_moves.GetComponent<move_text>().speed = orginal_speed + speed_add;
        }
        pressed = !pressed;
    }
    public void stop_cont()
    {
        if (pressed2)
        {
            text_moves.GetComponent<move_text>().speed = orginal_speed;
        }
        else
        {
            text_moves.GetComponent<move_text>().speed = 0f;
        }
        pressed2 = !pressed2;
    }
}
