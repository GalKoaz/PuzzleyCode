using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BunnyHelper : MonoBehaviour {
    [SerializeField] AllowedTiles allowedTiles;
    [SerializeField] TileBase bunnyHelperTile;
    [SerializeField] private TextMeshProUGUI bunnytext;

    private SpriteRenderer spriteRenderer;
    private bool previousSpriteStatus;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        previousSpriteStatus = spriteRenderer.enabled;
        UpdateAllowedTiles();
    }

    private void Update() {
        if (spriteRenderer.enabled != previousSpriteStatus) {
            previousSpriteStatus = spriteRenderer.enabled;
            bunnytext.color = Color.green;
            UpdateAllowedTiles();
        }
    }

    private void UpdateAllowedTiles() {
        if (spriteRenderer.enabled) {
            allowedTiles.AddTile(bunnyHelperTile);
        } else {
            allowedTiles.RemoveTile(bunnyHelperTile);
        }
    }
}