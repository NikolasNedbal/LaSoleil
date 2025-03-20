using UnityEngine;

public class LocationTracking : MonoBehaviour
{
    public BoxCollider2D cafeteria;
    public BoxCollider2D vinylStore;
    public BoxCollider2D groceryStore;

    [SerializeField]
    private GameObject openCloseCafeUi;

    private DayNight dn;

    private bool timeIsRight = false;

    public string playerLocation { get; private set; }

    [SerializeField]
    private GameObject openCloseBtn;
    private OpenCloseButton ocb;

    private void Awake()
    {
        ocb = openCloseBtn.GetComponent<OpenCloseButton>();
        dn = GameManager.Instance.gameObject.GetComponent<DayNight>();
    }

    private void Update()
    {

        if (dn.time > 21600 && dn.time < 79200)
        {
            timeIsRight = true;
        }
        else
        {
            timeIsRight = false;
        }

        if (cafeteria.OverlapPoint(GameManager.Instance.player.transform.position))
        {
            playerLocation = "Cafe";
            if (!openCloseCafeUi.activeSelf && timeIsRight)
            {
                openCloseCafeUi.SetActive(true);
            }
            else if (!timeIsRight && openCloseCafeUi.activeSelf)
            {
                ocb.EndOfTheDay();

                openCloseCafeUi.SetActive(false);
            }
        }
        else if (vinylStore.OverlapPoint(GameManager.Instance.player.transform.position))
        {
            playerLocation = "Vinyl";
            if (openCloseCafeUi.activeSelf)
            {
                openCloseCafeUi.SetActive(false);
            }

            if (!timeIsRight)
            {
                ocb.EndOfTheDay();
            }
        }
        else if (groceryStore.OverlapPoint(GameManager.Instance.player.transform.position))
        {
            playerLocation = "Store";
            if (openCloseCafeUi.activeSelf)
            {
                openCloseCafeUi.SetActive(false);
            }

            if (!timeIsRight)
            {
                ocb.EndOfTheDay();
            }
        }
    }
}
