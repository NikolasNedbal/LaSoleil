using UnityEngine;

public class LocationTracking : MonoBehaviour
{
    public BoxCollider2D cafeteria;
    public BoxCollider2D vinylStore;
    public BoxCollider2D groceryStore;

    [SerializeField]
    private GameObject openCloseCafeUi;

    public string playerLocation { get; private set; }

    private void Update()
    {
        if (cafeteria.OverlapPoint(GameManager.Instance.player.transform.position))
        {
            playerLocation = "Cafe";
            if (!openCloseCafeUi.activeSelf)
            {
                openCloseCafeUi.SetActive(true);
            }
        }
        else if (vinylStore.OverlapPoint(GameManager.Instance.player.transform.position))
        {
            playerLocation = "Vinyl";
            if (openCloseCafeUi.activeSelf)
            {
                openCloseCafeUi.SetActive(false);
            }
        }
        else if (groceryStore.OverlapPoint(GameManager.Instance.player.transform.position))
        {
            playerLocation = "Store";
            if (openCloseCafeUi.activeSelf)
            {
                openCloseCafeUi.SetActive(false);
            }
        }
    }
}
