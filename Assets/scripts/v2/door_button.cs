using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_button : MonoBehaviour
{
    public Camera m_MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                
                if (raycastHit.transform != null)
                {
                    //Our custom method. 
                    CurrentClickedGameObject(raycastHit.transform.gameObject);
                }
            }
        }
    }
    public void CurrentClickedGameObject(GameObject gameObject)
    {
        if (gameObject.CompareTag("drawer"))
        {
            gameObject.GetComponentInParent<LightsOutPuzzle>().UpdateOnMouseClick(gameObject);
            return;
        }
        
        if (gameObject.tag == "button")
        {
            gameObject.GetComponent<bulb_manager_3d>().click_animate();

            if (gameObject.GetComponent<bulb_manager_3d>().code_true)
            {
                gameObject.GetComponent<bulb_manager_3d>().open_doors();
                
            }

        }
        else if (gameObject.tag == "bulb")
        {
            gameObject.GetComponent<bulb3d>().Update_on_mouse_click();
        }
        if (gameObject.tag == "button2")
        {
            gameObject.GetComponent<bulb_manager_3d_logic>().click_animate();

            if (gameObject.GetComponent<bulb_manager_3d_logic>().code_true)
            {
                gameObject.GetComponent<bulb_manager_3d_logic>().open_doors();

            }

        }
        else if (gameObject.tag == "bulb2")
        {
            gameObject.GetComponent<bulb3dlogic>().Update_on_mouse_click();
        }
    }
}
