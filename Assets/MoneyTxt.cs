using TMPro;
using UnityEngine;

public class MoneyTxt : MonoBehaviour
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
        txt.text = GameManager.Instance.money.ToString();
    }
}
