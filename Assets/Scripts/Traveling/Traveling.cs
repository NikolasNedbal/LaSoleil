using UnityEngine;

public class Traveling : MonoBehaviour
{
    public Transform placeToGo;

    private Transform playerPos;

    private TravelUiManager uiManager;

    private string playerLoc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPress()
    {
        playerPos = GameManager.Instance.player.transform;

        if (playerPos != null)
        {
            playerPos.position = placeToGo.position;
        }

        GameManager.Instance.gameObject.GetComponent<DayNight>().SkipHour(2);
        CloseUi();
    }

    private void CloseUi()
    {
        foreach (var point in GameManager.Instance.doorPoints)
        {
            if (point != null && point.activeInHierarchy)
            {
                point.GetComponent<TravelUiManager>().CloseUi();
            }
        }
    }
}
