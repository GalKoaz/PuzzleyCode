using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulb : MonoBehaviour
{
    bool on = true;
    
    public Sprite off_p;
    public Sprite on_p;

    public int local_id;
    public bulb_manager b_m;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnMouseDown()
    {
        on = !on;
        Sprite pic = off_p;
        if (on)
        {
            pic = on_p;
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = pic;
        b_m.update_bulbs_number(local_id);
    }
}
