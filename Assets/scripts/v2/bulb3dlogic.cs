using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulb3dlogic : MonoBehaviour
{
    public bool on = false;
    public int local_id;
    public bulb_manager_3d_logic b_m;
    public Material off_m;
    public Material on_m;
    GameObject not_lemp;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = off_m;
    }
    public void create_not(GameObject bulb, int steps)
    {
        not_lemp = (GameObject)Instantiate(bulb);
        Vector3 temp = new Vector3(0f, -2f, 0f) * steps;
        not_lemp.GetComponent<Transform>().position = this.GetComponent<Transform>().position + temp;



    }
    // Update is called once per frame
    public void Update_on_mouse_click()
    {
        on = !on;
        if (on)
        {
            GetComponent<Renderer>().material = on_m;
        }
        else
        {
            GetComponent<Renderer>().material = off_m;
        }
        b_m.update_bulbs_number(local_id);
        not_lemp.GetComponent<bulb3dlogic>().Update_on_mouse_click_logic();
    }
    public void Update_on_mouse_click_logic()
    {
        
        on = !on;
        if (on)
        {
            Debug.Log("dsdsdsd");
            GetComponent<Renderer>().material = on_m;
        }
        else
        {
            GetComponent<Renderer>().material = off_m;
        }
    }
}
