using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    public float speed = 10f;
    public string next_scene;
    public string tag_player = "Player";
    public GameObject swirl;
    private Animator animator;
    // Start is called before the first frame update ofek
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == tag_player)
        {
            Debug.Log(this.name + " Trigger 3D with " + other.name + " tag=" + other.tag);
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        //Rotate 90 deg
        animator.SetBool("door_open", true);
        yield return new WaitForSeconds(0.5f);
        Instantiate(swirl);
        //Wait for 4 seconds
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(next_scene);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
