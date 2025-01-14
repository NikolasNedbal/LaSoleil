using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantManager : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject player;
    public GameObject plantPrefab;
    public Transform[] availablePlantSpots;
    public Item plantItem;
    public Item harvestedItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlantOnPlayerTile();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            HarvestOnPlayerTile();
        }
    }

    private void PlantOnPlayerTile()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3Int tilePosition = tilemap.WorldToCell(playerPosition);

        if (!IsPlantOnTile(tilePosition) && GameManager.Instance.invContainer.HasItem("plantSeed"))
        {
            GameManager.Instance.invContainer.Remove(plantItem, 1);
            GameObject newPlant = Instantiate(plantPrefab, tilemap.CellToWorld(tilePosition), Quaternion.identity);

            Plant plantScript = newPlant.GetComponent<Plant>();
            plantScript.tilemap = tilemap;
            plantScript.plantPosition = tilePosition;

            GameManager.Instance.plants.Add(plantScript);
        }
        else
        {
            Debug.Log("Cannot plant here.");
        }
    }

    private void HarvestOnPlayerTile()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3Int tilePosition = tilemap.WorldToCell(playerPosition);

        Plant plantToHarvest = FindPlantOnTile(tilePosition);

        if (plantToHarvest != null && plantToHarvest.IsFullyGrown())
        {
            GameManager.Instance.plants.Remove(plantToHarvest);
            tilemap.SetTile(tilePosition, null);
            Destroy(plantToHarvest.gameObject);

            GameManager.Instance.invContainer.Add(harvestedItem, 1);
        }
        else
        {
            Debug.Log("No fully grown plant to harvest here.");
        }
    }

    private bool IsPlantOnTile(Vector3Int tilePosition)
    {
        foreach (Plant plant in GameManager.Instance.plants)
        {
            if (plant.plantPosition == tilePosition)
                return true;
        }
        return false;
    }

    private Plant FindPlantOnTile(Vector3Int tilePosition)
    {
        foreach (Plant plant in GameManager.Instance.plants)
        {
            if (plant.plantPosition == tilePosition)
                return plant;
        }
        return null;
    }
}
