using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class ShipCollisionHandler : Keycut
{
    // Variables to keep track of player's interaction with the ship
    private bool playerTouchingShip;
    private bool hidden;
    private SpriteRenderer playerSpriteRenderer;
    private KeyboardMoverByTile shipControlScript;
    private ClickMoverDijkstra dijkstraMoverScript;

    // Serialized fields for user-defined input
    [SerializeField] GameObject player;
    [SerializeField] Tilemap tilemap;
    [SerializeField] AllowedTiles allowedTiles;
    [SerializeField] TileBase[] AllowedCut;
    [SerializeField] float slow = 1f;

    // Variable to track the current time
    private float currTime = 0f;

    private void Start()
    {
        // Get the ship control and Dijkstra mover scripts from the GameObject
        shipControlScript = gameObject.GetComponent<KeyboardMoverByTile>();
        dijkstraMoverScript = gameObject.GetComponent<ClickMoverDijkstra>();
    }

    // Get the tile at the given world position
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collided object is the player and update the variables
        if (collision.gameObject == player)
        {
            Debug.Log("Player entered ship trigger");
            playerTouchingShip = true;
            playerSpriteRenderer = collision.GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        // Hide the player sprite when the player is touching the ship and the sprite is not hidden yet
        if (playerTouchingShip && playerSpriteRenderer != null && hidden == false)
        {
            Debug.Log("Hiding player sprite");
            player.SetActive(false);
            hidden = true;
            shipControlScript.enabled = true;
            dijkstraMoverScript.enabled = true;
        }
        
        // Update the position based on user input
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);

        // Check if the new position is on an allowed tile
        if (allowedTiles.Contain(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("You cannot ship on " + tileOnNewPosition + "!");
        }

        // Check if the user pressed the 'G' key
        if (Input.GetKeyDown(KeyCode.G))
        {
            // Get the tile at the new position
            TileBase tileOnDirPosition = TileOnPosition(transform.position + saveStep);

            // Check if enough time has passed since the last interaction
            if (Time.time > currTime + slow)
            {
                currTime = Time.time;
            }


            // Check if the tile at the new position is allowed to be cut
            if (AllowedCut.Contains(tileOnDirPosition))
            {
                // Calculate the new player position by adding twice the saveStep to the current position
                Vector3 playerPos = transform.position + saveStep + saveStep;
                Vector3Int cellPosition = tilemap.WorldToCell(playerPos);

                // Convert cell position to world position and add an offset for tile size
                Vector3 worldPosition = tilemap.CellToWorld(cellPosition);
                Vector3 tileSizeOffset = new Vector3(tilemap.cellSize.x / 2, tilemap.cellSize.y / 2, 0);

                // Activate the player game object and set its position to the calculated world position with offset
                player.SetActive(true);
                player.transform.position = worldPosition + tileSizeOffset;

                // Reveal the player sprite (commented out, as 'player.SetActive(true)' already makes the player visible)
                // playerSpriteRenderer.enabled = true;

                // Update state variables to reflect that the player is no longer hidden or touching the ship
                hidden = false;
                shipControlScript.enabled = false;
                dijkstraMoverScript.enabled = false;
                playerTouchingShip = false;
            }


        }
        
    }
}