using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour
{
    private NavMeshAgent agent;
    private Chair chairToSit;

    private bool playerActive = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false; //NEMAZAT jinak zmizí sprite
        GoToCashRegister();
    }

    private void GoToCashRegister()
    {
        agent.SetDestination(GameManager.Instance.cashRegister.position);
    }

    // Update is called once per frame
    void Update()
    {
       if(playerActive && Input.GetKeyDown(KeyCode.F))
        {
            if(chairToSit == null)
            {
                GoToTable();
            }
            else
            {

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerActive = true;
        }
        else
        {
            playerActive = false;
        }
    }

    public void GoToTable()
    {
        chairToSit = GetAvailableChair();
        if (chairToSit != null) 
        {
            chairToSit.ocupied = true;
            agent.SetDestination(chairToSit.transform.position);
        }
        else
        {
            Debug.Log("NoChairAvailable");
        }
    }

    Chair GetAvailableChair()
    {
        foreach (Table table in GameManager.Instance.tables) 
        {
            Chair availableChair = table.GetAvailableChair();
            if(availableChair != null)
            {
                return availableChair;
            }
        }
        return null;
    }
}
