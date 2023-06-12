using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulb_manager : MonoBehaviour
{
    public GameObject prefab_level;
    public int length;
    public int steps;
    public int shift;

    public bool show = true;
    bool[] number;
    public bool move_page = true;
    public string back_scene;
    public int win_number;
    // Start is called before the first frame update
    void Start()
    {
        number = new bool[length];
        // Get the position of the left and right edges of the game window in world coordinates
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0.5f, 0f));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0.5f, 0f));

        // Calculate the width of the world
        float worldWidth = Vector3.Distance(leftEdge, rightEdge);
        Vector3 temp = this.gameObject.GetComponent<Transform>().position;

        for (int j = 0; j < length; j++)
        {
            GameObject level_p = (GameObject)Instantiate(prefab_level);
            Vector3 movement = new Vector3(j * steps - (worldWidth + shift) / 2, temp.y, 0);
            level_p.GetComponent<bulb>().b_m = this;
            level_p.GetComponent<bulb>().local_id = j;
            level_p.GetComponent<Transform>().position = movement;
            number[j] = true;
        }
        update_number();

    }
    void update_number()
    {
        int num = 0;
        int runner = 0;
        for (int i = length - 1; i >= 0; i--)
        {
            if (number[i])
            {
                num += (int)Math.Pow(2, runner);
            }
            runner++;
        }
        if (show)
        {
            this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "number is: " + num;
        }
        else
        {
            this.gameObject.transform.GetChild(0).GetComponent<TextMesh>().text = "";
        }
        if (num == win_number)
        {
            if (this.move_page)
            {
                SceneManager.LoadScene(back_scene);
            }
        }

    }
    public void update_bulbs_number(int index)
    {
        number[index] = !number[index];
        update_number();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
