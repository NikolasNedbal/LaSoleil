using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    Tile closedDoorTop;
    [SerializeField]
    Tile closedDoorBottom;
    [SerializeField]
    Tile openDoorTop;
    [SerializeField]
    Tile openDoorBottom;

    public Vector3Int topDoorPos;
    public Vector3Int bottomDoorPos;

    private bool isPlayerInside = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            UpdateDoorState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            UpdateDoorState();
        }
    }

    private void UpdateDoorState()
    {
        if (isPlayerInside)
        {
            tilemap.SetTile(topDoorPos, openDoorTop);
            tilemap.SetTile(bottomDoorPos, openDoorBottom);
        }
        else
        {
            tilemap.SetTile(topDoorPos, closedDoorTop);
            tilemap.SetTile(bottomDoorPos, closedDoorBottom);
        }
    }
}
