using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AllowWithBunny : MonoBehaviour
{
    [SerializeField] TileBase[] allowedTiles = null;
    [SerializeField] GameObject bunnyGameObject = null;
    [SerializeField] TileBase bunnyHelperTile = null;

    private SpriteRenderer bunnySpriteRenderer;

    private void Start()
    {
        bunnySpriteRenderer = bunnyGameObject.GetComponent<SpriteRenderer>();
    }

    public bool Contain(TileBase tile)
    {
        if (bunnySpriteRenderer.enabled)
        {
            // If the Bunny's SpriteRenderer is enabled and the tile is the specific bunnyHelperTile, return true
            if (tile == bunnyHelperTile)
            {
                return true;
            }
        }
        return allowedTiles.Contains(tile);
    }

    public TileBase[] Get() { return allowedTiles; }
}