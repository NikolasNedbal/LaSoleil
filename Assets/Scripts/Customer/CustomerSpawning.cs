using System;
using System.Collections;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    private float cooldown = 6.5f;
    private int maxCustomers = 4;
    private int curCustomers;
    //[SerializeField]
    private Transform spawnPoint;
    //[SerializeField]
    private GameObject customer;

    private bool canSpawnCustomers = false;

    private DayNight dn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        dn = GameManager.Instance.gameObject.GetComponent<DayNight>();
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
        if (canSpawnCustomers && dn.time > 21600 && dn.time < 79200)
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

    public void RemoveCustomer()
    {
        Debug.Log("AEFJIOFOAIEJFOI");
        curCustomers--;
    }
}
