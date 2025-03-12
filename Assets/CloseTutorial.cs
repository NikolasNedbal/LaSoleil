using UnityEngine;

public class CloseTutorial : MonoBehaviour
{
    public GameObject tut;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press1()
    {
        tut.gameObject.SetActive(false);
    }

    public void Press2()
    {
        tut.gameObject.SetActive(true);
    }
}
