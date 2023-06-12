using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapWGraph : IWGraph<Vector3Int>
{
    private Tilemap tilemap;
    private AllowedTilesW allowedTiles;

    // Constructor takes a Tilemap and AllowedTilesW objects as arguments
    public TilemapWGraph(Tilemap tilemap1, AllowedTilesW allowedTiles1)
    {
        this.tilemap = tilemap1;
        this.allowedTiles = allowedTiles1;
    }

    // Method to get the weight of a node (tile) in the graph
    public int getW(Vector3Int node)
    {
        TileBase tb = tilemap.GetTile(node);

        // Return the weight of the tile from the allowedTiles object
        return allowedTiles.GetWeight(tb);
    }

    // Define the possible directions for neighbors (up, down, left, and right)
    static Vector3Int[] directions = {
        new Vector3Int(-1, 0, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0),
        new Vector3Int(0, 1, 0),
    };

    // Method to return the neighbors of a given node (tile) in the graph
    public IEnumerable<Vector3Int> Neighbors(Vector3Int node)
    {
        // Loop through each direction to check for neighboring tiles
        foreach (var direction in directions)
        {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            // If the neighborTile is in the list of allowed tiles, return its position
            if (allowedTiles.Get().Contains(neighborTile))
                yield return neighborPos;
        }
    }
}