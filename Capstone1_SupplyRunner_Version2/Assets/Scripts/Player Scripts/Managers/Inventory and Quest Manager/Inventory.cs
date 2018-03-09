using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Handles all inventory functions
/// </summary>
[RequireComponent(typeof(InventoryUI))]
public class Inventory : MonoBehaviour
{

    private static Inventory instance;

    public static Inventory Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType<Inventory>();
                if (!instance)
                {
                    GameObject newInstance = new GameObject("Inventory");
                    instance = newInstance.AddComponent<Inventory>();
                }
            }
            return instance;
        }
    }

    [Tooltip("Maximum number of unique items")]
    public int MaximumItemSlots;
    [Tooltip("List of items in the inventory")]
    public List<Item> Items;

    //List of unique items in the inventory
    [HideInInspector]
    public List<Item> uniqueItems;

    private InventoryUI inventoryUIReference;


    // Use this for initialization
    void Start()
    {

        inventoryUIReference = GetComponent<InventoryUI>();
    }

    // Update is called once per frame
    void Update()
    {

        //Removes item in the list of item by finding items that are null
        Items.Remove(Items.Find(x => x == null));

        //Get a list of items that contains unique item
        uniqueItems = Items.Distinct().ToList();

        SortInventory();
    }

    void SortInventory()
    {
        //Sorts item alphabetical
        Items.Sort((x, y) => x.ItemName.CompareTo(y.ItemName));
    }

    //Adds an item to the list of items in the inventory
    public void AddItem(Item itemObject)
    {
        //If the number of unique items is less than maximum item slots

        if (uniqueItems.Count < MaximumItemSlots || IsItemExist(itemObject))
        {
            //Add the item to the list of item
            Items.Add(itemObject);

            //Get a list of items that contains unique item
            uniqueItems = Items.Distinct().ToList();

            inventoryUIReference.UpdateUI();
        }
    }

    public void RemoveItem(Item item)
    {
        //Find the item to remove in the list of items
        Item itemToRemove = Items.Find(x => x == item);

        //If the item to remove exists
        if (itemToRemove != null)
        {
            Items.Remove(itemToRemove);
        }
        inventoryUIReference.UpdateUI();
    }

    public int GetNumberOfItem(Item item)
    {
        int numberofItems = 0;
        foreach (Item items in Items)
        {
            if (items == item)
            {
                numberofItems++;
            }
        }
        return numberofItems;
    }

    public int GetNumberOfItem(string itemName)
    {
        int numberofItems = 0;
        Item itemToFind = Items.Find(x => x.ItemName.ToLower() == itemName.ToLower());
        foreach (Item items in Items)
        {
            if (items == itemToFind)
            {
                numberofItems++;
            }
        }
        return numberofItems;
    }

    public bool IsItemExist(string itemName)
    {
        return Items.Exists(x => x.ItemName == itemName);
    }

    public bool IsItemExist(Item item)
    {
        return Items.Exists(x => x == item);
    }
}
