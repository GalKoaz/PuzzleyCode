using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCollision : MonoBehaviour
{
    // Reference to the player GameObject
    [SerializeField] GameObject player;
    // Reference to the Cutting script on the player
    private Cutting CuttingScript;

    // Start is called before the first frame update
    private void Start()
    {
        // Get the Cutting script from the player GameObject
        CuttingScript = player.GetComponent<Cutting>();
        // Disable the Cutting script initially
        CuttingScript.enabled = false;
    }

    // OnTrigger function called when the slime collider is touched by another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is the player
        if (collision.gameObject == player)
        {
            // Log that the player has touched the slime trigger
            Debug.Log("Player touches slime trigger");
            // Enable the Cutting script on the player
            CuttingScript.enabled = true;
            // Disable the slime GameObject (this will also disable this script)
            gameObject.SetActive(false);
        }
    }
}