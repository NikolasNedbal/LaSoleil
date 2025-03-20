using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public int money;

    public int days;
    public int weeks;

    public int rent;

    public List<Plant> plants;

    public List<CoffeeItem> menuItems = new List<CoffeeItem>();
    public Table[] tables;
    public Transform cashRegister;

    public Transform SpawnPoint;
    public GameObject customer;

    public bool isCafeOpen;

    public GameObject player;
    public ItemContainer invContainer;
    public ItemContainer storageContainer;
    public ItemContainer ferContainer;
    public ItemContainer grContainer;

    public InventoryPanel invPanel;
    public InventoryPanel storagePanel;
    public InventoryPanel ferPanel;
    public InventoryPanel grPanel;

    public Canvas ui;

    public GameObject[] doorPoints;

    public TextMeshProUGUI interTxt;

    public bool isFerInProcess;
    public void RemoveMoney(int value)
    {
        money -= value;
    }
    public void GrowPlants()
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
