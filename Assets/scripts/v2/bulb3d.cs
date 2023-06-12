using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulb3d : MonoBehaviour
{
    bool on = false;
    public int local_id;
    public bulb_manager_3d b_m;
    public Material off_m;
    public Material on_m;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = off_m;
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
    }
}
