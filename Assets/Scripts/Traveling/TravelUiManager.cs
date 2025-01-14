using UnityEngine;

public class TravelUiManager : MonoBehaviour
{
    public GameObject ui;
    private bool isPlayerActive = false;

    // Update is called once per frame
    void Update()
    {
        if(isPlayerActive && Input.GetKeyUp(KeyCode.F))
        {
            ui.SetActive(!ui.activeSelf);
        }
    }

    public void CloseUi()
    {
        ui.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if( collision.tag == "Player")
        {
            isPlayerActive = false;
        }
    }
}
