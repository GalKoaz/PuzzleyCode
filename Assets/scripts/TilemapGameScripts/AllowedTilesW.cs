using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AllowedTilesW : MonoBehaviour
{
    // SerializeField attributes to make private fields editable in the Unity Inspector
    [SerializeField] private Dictionary<TileBase, int> dic;
    [SerializeField] private TileBase[] allowedTiles;
    [SerializeField] private int[] weight;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize the dictionary
        dic = new Dictionary<TileBase, int>();

        // Populate the dictionary with allowedTiles and their respective weights
        for (int i = 0; i < allowedTiles.Length && i < weight.Length; i++)
        {
            dic.Add(allowedTiles[i], weight[i]);
        }
    }

    // Check if the given tile is in the allowedTiles array
    public bool Contain(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    // Return the allowedTiles array
    public TileBase[] Get() { return allowedTiles; }

    // Return the weight of a given tile from the dictionary
    public int GetWeight(TileBase tile) { return dic[tile]; }
}