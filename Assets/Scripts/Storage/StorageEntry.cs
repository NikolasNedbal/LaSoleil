using UnityEngine;

public class StorageEntry : MonoBehaviour
{
    private BoxCollider2D col;
    private bool playerActive = false;
    public GameObject canvas;

    [SerializeField] Canvas invPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.SetActive(false);
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerActive && Input.GetKeyUp(KeyCode.F))
        {
            invPanel.gameObject.SetActive(true);
            canvas.SetActive(!canvas.activeSelf);
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
            canvas.gameObject.SetActive(false);
        }
    }
}
