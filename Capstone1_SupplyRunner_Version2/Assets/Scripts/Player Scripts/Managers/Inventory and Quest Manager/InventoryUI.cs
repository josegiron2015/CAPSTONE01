using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Handles UI for inventory and user inputs
/// </summary>
//[RequireComponent(typeof(Inventory))]
public class InventoryUI : MonoBehaviour
{

    [Tooltip("List of items")]
    public List<Slot> Slots;

    //Reference to inventory script
    private Inventory inventoryReference;

    // Use this for initialization
    void Start()
    {
        //If list is empty
        if (Slots == null)
        {
            // find all objects with Slot script
            Slots = FindObjectsOfType<Slot>().ToList();
        }
        //Get inventory script
        inventoryReference = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateUI()
    {
        Debug.Log("TET");   
        //For each item in the unique item list in inventory script
        foreach (Item item in inventoryReference.uniqueItems)
        {
            //For each slot in the list of item slots
            foreach (Slot itemSlot in Slots)
            {
                //if the item slot currently has an item data
                //and it is the same item in the current iteration
                if (itemSlot.ItemData == item)
                {
                    //Increase the quantity of the existing item
                    itemSlot.SetQuantity(inventoryReference.GetNumberOfItem(itemSlot.ItemData));

                    itemSlot.UpdateItemSlot();
                    break ;
                }
                //Else if the item data is null
                else if(itemSlot.ItemData == null)
                {
                    Debug.Log("Placed " + item.ItemName + " in slot " + itemSlot.name);
                    //Set the current item data to the current
                    //item in the iteration
                    itemSlot.ItemData = item;

                    itemSlot.UpdateItemSlot();
                    break ;
                }
            }

        }
    }
}
