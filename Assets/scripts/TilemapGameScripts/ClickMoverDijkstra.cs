using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ClickMoverDijkstra inherits from TargetMoverDijkstra class
public class ClickMoverDijkstra : TargetMoverDijkstra
{
    // SerializeField attribute to make the private field editable in the Unity Inspector
    [SerializeField] private int MouseR = 0;

    // Update is called once per frame
    void Update()
    {
        // Check if the specified mouse button (MouseR) is pressed
        if (Input.GetMouseButtonDown(MouseR))
        {
            // Convert the mouse position from screen space to world space
            Vector3 newTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Set the target position for the Dijkstra algorithm
            SetTarget(newTarget);
        }
    }
}