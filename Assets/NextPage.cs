using UnityEngine;

public class NextPage : MonoBehaviour
{
    public GameObject toClose;
    public GameObject toOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        toClose.SetActive(false);
        toOpen.SetActive(true);
    }
}
