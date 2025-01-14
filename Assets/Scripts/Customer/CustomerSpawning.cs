using System;
using System.Collections;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    private float cooldown = 5f;
    private int maxCustomers = 12;
    private int curCustomers;
    //[SerializeField]
    private Transform spawnPoint;
    //[SerializeField]
    private GameObject customer;

    private bool canSpawnCustomers = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        customer = GameManager.Instance.customer;
        spawnPoint = GameManager.Instance.SpawnPoint;
    }

    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    // Update is called once per frame
    void Update()
    {
        canSpawnCustomers = GameManager.Instance.isCafeOpen;
        Debug.Log(canSpawnCustomers);
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void SpawnCustomer()
    {
        if (canSpawnCustomers)
        {
            if (curCustomers < maxCustomers)
            {
                curCustomers++;
                Instantiate(customer, spawnPoint);
            }
            else
            {
                Debug.Log("Max Customers Reached");
            }
        }
        else
        {
            Debug.Log("Cafe is not open");
        }
    }
}
