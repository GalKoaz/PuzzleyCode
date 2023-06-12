using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AllowedTiles : MonoBehaviour {
    [SerializeField] TileBase[] allowedTiles = null;

    public bool Contain(TileBase tile) {
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedTiles; }

    public void AddTile(TileBase tileToAdd) {
        if (!allowedTiles.Contains(tileToAdd)) {
            allowedTiles = allowedTiles.Concat(new[] { tileToAdd }).ToArray();
        }
    }

    public void RemoveTile(TileBase tileToRemove) {
        allowedTiles = allowedTiles.Where(tile => tile != tileToRemove).ToArray();
    }
}