using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldgen : MonoBehaviour
{
    public string[] scene_levels;
    public GameObject prefab_level;
    public int length;
    public int steps;
    public int shift;
    // Start is called before the first frame update
    void Start()
    {

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
            level_p.GetComponent<ClickMoveScene>().next_scene = scene_levels[j];
            level_p.GetComponent<Transform>().position = movement;
            level_p.GetComponent<ClickMoveScene>().change_number();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
