using TMPro;
using UnityEngine;

public class RentTxt : MonoBehaviour
{
    private TextMeshProUGUI txt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        txt = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = GameManager.Instance.rent.ToString();
    }
}
