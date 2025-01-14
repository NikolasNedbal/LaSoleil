using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        /*if(dialogue == null)
        {
            dialogue = gameObject.GetComponent<DialogueController>();
        }*/
    }

    public int money;

    public List<Plant> plants;

    public List<CoffeeItem> menuItems = new List<CoffeeItem>();

    //public DialogueController dialogue;

    public bool isCafeOpen;

    public GameObject player;
    public ItemContainer invContainer;
    public ItemContainer storageContainer;

    public InventoryPanel invPanel;
    public InventoryPanel storagePanel;

    public Table[] tables;
    public Transform cashRegister;

    public Transform SpawnPoint;
    public GameObject customer;

    public Canvas ui;

    public int days;
    public int weeks;

    public GameObject[] doorPoints; 

    public void RemoveMoney(int value)
    {
        money -= value;
    }
    public void NewDay()
    {
        foreach (Plant plant in plants)
        {
            if (!plant.IsFullyGrown())
            {
                plant.GrowPlant();
            }
        }
    }
}
