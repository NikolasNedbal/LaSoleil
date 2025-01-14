using UnityEngine;
using UnityEngine.Tilemaps;

public class Plant : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase[] plantStages;
    public Vector3Int plantPosition;
    public int growthStage = 0;

    private void Start()
    {
        plantPosition = tilemap.WorldToCell(transform.position);  // Convert world position to tilemap grid position
        UpdatePlantSprite();
    }

    public void GrowPlant()
    {
        if (growthStage < 7)
        {
            growthStage++;
            UpdatePlantSprite();
        }
    }

    private void UpdatePlantSprite()
    {
        if (growthStage < plantStages.Length)
        {
            tilemap.SetTile(plantPosition, plantStages[growthStage]);
        }
    }
    public bool IsFullyGrown()
    {
        return growthStage >= 7;
    }
}
