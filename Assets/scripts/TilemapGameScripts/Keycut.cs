using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycut : MonoBehaviour
{
    protected Vector3 saveStep; // The last movement direction of the player

    // Calculate the new position based on arrow key input
    protected Vector3 NewPosition()
    {
        // If the left arrow key is pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            saveStep = Vector3.left; // Save the movement direction
            return transform.position; // Return the current position
        }
        // If the right arrow key is pressed
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            saveStep = Vector3.right; // Save the movement direction
            return transform.position; // Return the current position
        }
        // If the down arrow key is pressed
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveStep = Vector3.down; // Save the movement direction
            return transform.position; // Return the current position
        }
        // If the up arrow key is pressed
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            saveStep = Vector3.up; // Save the movement direction
            return transform.position; // Return the current position
        }
        else
        {
            return transform.position; // If no arrow key is pressed, return the current position
        }
    }

    void Update()
    {
        // Update the player's position based on the arrow key input
        transform.position = NewPosition();
    }
}