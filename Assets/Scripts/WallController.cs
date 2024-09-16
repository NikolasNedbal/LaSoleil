using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] Color transparentColor;
    [SerializeField] Color originalColor;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateTilemapColor(transparentColor);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateTilemapColor(originalColor);
        }
    }

    private void UpdateTilemapColor(Color color)
    {
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        foreach (var position in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(position);
            if (tile != null)
            {
                Tile tileWithColor = ScriptableObject.CreateInstance<Tile>();
                tileWithColor.sprite = (tile as Tile)?.sprite;
                tileWithColor.color = color;

                tilemap.SetTile(position, tileWithColor);
            }
        }
    }
}
