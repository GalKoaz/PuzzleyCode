using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TargetMoverDijkstra : MonoBehaviour
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTilesW allowedTiles = null;
    [SerializeField] float speed = 2f;
    [SerializeField] int maxIterations = 1000;
    [SerializeField] Vector3 targetInWorld = new Vector3(-4,-1,0);
    [SerializeField] Vector3Int targetInGrid = new Vector3Int(-4,-1,0);

    private int[] weights;
    protected bool atTarget;
    private int step = 1;

    // Set the target position in world coordinates and grid coordinates
    // This method will update the target position if it's different from the current one
    public void SetTarget(Vector3 newTarget)
    {
        if (targetInWorld != newTarget)
        {
            targetInWorld = newTarget;
            targetInGrid = tilemap.WorldToCell(targetInWorld);
            atTarget = false;
        }
    }

    // Get the current target position in world coordinates
    public Vector3 GetTarget()
    {
        return targetInWorld;
    }

    private TilemapWGraph tilemapGraph = null;
    [SerializeField] private float timeBetweenSteps;

    protected virtual void Start()
    {
        // Initialize the TilemapWGraph object and calculate the time between steps
        tilemapGraph = new TilemapWGraph(tilemap, allowedTiles);
        timeBetweenSteps = step / speed;
        StartCoroutine(MoveTowardsTheTarget());
    }

    // Coroutine that moves the object towards the target
    IEnumerator MoveTowardsTheTarget()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            if (enabled && !atTarget)
                MakeOneStepTowardsTheTarget();
        }
    }

    // Makes one step towards the target using Dijkstra's algorithm
    private void MakeOneStepTowardsTheTarget()
    {
        // Convert the object's current position to grid coordinates
        Vector3Int startNode = tilemap.WorldToCell(transform.position);
        // Use the targetInGrid as the destination node in grid coordinates
        Vector3Int endNode = targetInGrid;

        // Get the shortest path using Dijkstra's algorithm
        List<Vector3Int> shortestPath = Dijkstra.GetPath(tilemapGraph, startNode, endNode);
        Debug.Log("shortestPath = " + string.Join(" , ", shortestPath));

        // If there are at least 2 nodes in the path, it means there's a valid next step
        if (shortestPath.Count >= 2)
        {
            // Get the next node in the path
            Vector3Int nextNode = shortestPath[1];
            // Move the object to the center of the next node in world coordinates
            transform.position = tilemap.GetCellCenterWorld(nextNode);
            // Calculate the time between steps based on the tile's weight
            timeBetweenSteps = GetNewTimeBetweenSteps(tilemap.GetTile(nextNode));
        }
        else
        {
            // If there's no valid next step, the object has reached the target
            atTarget = true;
        }
    }

    // Calculate the new time between steps based on the weight of the current tile
    private float GetNewTimeBetweenSteps(TileBase currentTile)
    {
        return 1 / (speed * step / allowedTiles.GetWeight(currentTile));
    }
}
