using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Cutting : Keycut
{
    [SerializeField] Tilemap tilemap; // The tilemap object
    [SerializeField] AllowedTiles allowedTiles; // Reference to allowed tiles for movement
    [SerializeField] TileBase[] AllowedCut; // Tiles that can be cut
    [SerializeField] GameObject tree; // Tree prefab to instantiate after cutting a tile
    [SerializeField] TileBase afterCut; // Tile that replaces the cut tile
    [SerializeField] float slow = 1f; // Time interval between cuts
    private float currTime = 0f; // The current time
    private bool canCat = true; // Flag indicating whether the player can cut or not

    // Get the tile at the given world position
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()
    {
        // Calculate the new position of the player based on input
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);

        // If the tile at the new position is allowed for movement, update the player's position
        if (allowedTiles.Contain(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }

        // If the E key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            TileBase tileOnDirPosition = TileOnPosition(transform.position + saveStep);

            // Check if the player can cut by comparing the time interval
            if (Time.time > currTime + slow)
            {
                currTime = Time.time;
                canCat = true;
            }

            // If the tile can be cut and the player can cut
            if (AllowedCut.Contains(tileOnDirPosition) && canCat)
            {
                Vector3 playerPos = transform.position + saveStep;
                Vector3Int cellPosition = tilemap.WorldToCell(playerPos);
                tilemap.SetTile(cellPosition, afterCut);

                // Instantiate a new GameObject from the serialized field at the same position as the tile
                Vector3 worldPosition = tilemap.CellToWorld(cellPosition);
                Vector3 tileSizeOffset = new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, 0);
                GameObject newTree = Instantiate(tree, worldPosition + tileSizeOffset, Quaternion.identity);
            }

            canCat = false; // Set the canCat flag to false for the next frame
        }
    }
}
