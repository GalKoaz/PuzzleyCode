using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class talking : MonoBehaviour
{
    public string[] text;
    public TMP_Text canvas_text;
    public Image keyImage;
    int index = 0;
    bool inside = false;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag == "Player")
        {
            inside = true;
            keyImage.enabled = true;
            canvas_text.text = "";
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        while(index < text.Length && inside)
        {
            canvas_text.text = text[index];
            index++;
        
            yield return new WaitForSeconds(5f);
    
          
        }
        yield break;

    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.tag);

        if (other.tag == "Player")
        {
            StopCoroutine(waiter());
            inside = false;
            index = 0;
            keyImage.enabled = false;
            canvas_text.text = "";
        }
    }


}
