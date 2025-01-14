using UnityEngine;

public class OpenStoreWindow : MonoBehaviour
{
    [SerializeField]
    private Canvas storeUi;

    private bool playerActive = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerActive && Input.GetKeyUp(KeyCode.F))
        {
            storeUi.gameObject.SetActive(!storeUi.gameObject.activeInHierarchy);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerActive = false;
        }
    }
}
