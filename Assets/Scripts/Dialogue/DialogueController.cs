using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private GameObject dialoguePanel;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private Button acceptButton;
    [SerializeField]
    private Button declineButton;

    public Customer customer;
    public bool wasAccepted = false;
    public bool wasDeclined = false;

    private string dialogue;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //dialoguePanel.SetActive(false);
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (customer != null) 
        {
            string milkos;
            if (customer.customerPreferences[0].milk == true)
            {
                milkos = "With milk";
            }
            else
            {
                milkos = "Without milk";
            }
            dialogue = ("Good day, I'd like to order " + customer.customerPreferences[0].Name + " Sugar spoons: " + customer.customerPreferences[0].sugarSpoons + " " + milkos);
            dialogueText.text = dialogue;
            acceptButton.gameObject.SetActive(true);
            declineButton.gameObject.SetActive(true);
        }
    }

    public void OnAcceptPress()
    {
        wasAccepted = true;
        TurnOnDialogue(false);
    }

    public void OnDeclinePress() 
    {
        wasAccepted = false;
        TurnOnDialogue(false);
        wasDeclined = true;
    }

    public void TurnOnDialogue(bool state)
    {
        dialoguePanel.SetActive(state);
    }

    public void ResetOrder(bool state)
    {
        wasAccepted = state;
        wasDeclined = state;
    }
}
