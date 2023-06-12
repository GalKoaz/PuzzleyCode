using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyHelperCollision : MonoBehaviour
{
    [SerializeField] GameObject player; // Reference to the player GameObject
    [SerializeField] private GameObject BunnyHelper; // Reference to the SpriteRenderer component of the BunnyHelper

    // OnTrigger function called when the slime collider is touched by another collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collider is the player
        if (collision.gameObject == player)
        {

            BunnyHelper.GetComponent<SpriteRenderer>().enabled = true;
            // Log that the player has touched the slime trigger
            Debug.Log("Player touches slime trigger");

            // Disable the slime GameObject (this will also disable this script)
            gameObject.SetActive(false);

            // Enable the SpriteRenderer component of the BunnyHelper
        }
    }
}