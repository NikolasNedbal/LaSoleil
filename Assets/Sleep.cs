using UnityEngine;

public class Sleep : MonoBehaviour
{
    private DayNight dn;
    private bool playerActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dn = GameManager.Instance.gameObject.GetComponent<DayNight>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActive && dn != null && Input.GetKeyDown(KeyCode.F))
        {
            dn.NewDay();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActive = false;
        }
    }
}
