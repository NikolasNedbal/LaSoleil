using System;
using TMPro;
using UnityEngine;

public class OpenCloseButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI btnTxt;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnTxt.text = "Open";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPress()
    {
        anim.SetTrigger("ButtonClick");
        if (!GameManager.Instance.isCafeOpen)
        {
            GameManager.Instance.isCafeOpen = true;
            btnTxt.text = "Close";
            Debug.Log("Cafe is open");
        }
        else
        {
            GameManager.Instance.isCafeOpen = false;
            btnTxt.text = "Open";
            Debug.Log("Cafe is closed");
        }
    }

    public void EndOfTheDay()
    {
        if (GameManager.Instance.isCafeOpen)
        {
            GameManager.Instance.isCafeOpen = false;
            btnTxt.text = "Open";
            Debug.Log("End of the day = Closed");
        }  
    }
}
