using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{

    public Item ItemData;
    public Button Icon;
    public TextMeshProUGUI ItemNumber;
    public int ItemQuantity;

    private Sprite origSprite;
    // Use this for initialization
    void Start()
    {
        origSprite = Icon.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateItemSlot();

        if (ItemData)
        {
            if (ItemData.IsUseable)
            {
                Icon.interactable = true;
            }
            else
                Icon.interactable = false;
        }


    }

    public void UpdateItemSlot()
    {
        //If there is an item in this slot
        if (ItemData)
        {
            //Set the icon of the sprite and increase the quantity
            Icon.GetComponent<Image>().sprite = ItemData.ItemSprite;
            //AddQuantity();
        }
        //If there is no item in this slot
        else
        {
            //Set the icon of the sprite to null
            Icon.GetComponent<Image>().sprite = origSprite;
            //And set quantity to 0
            ItemQuantity = 0;
        }

        //Set the item number text based on the quantity
        ItemNumber.text = (ItemQuantity > 0) ? ItemQuantity.ToString() : " ";
    }

    public void SetQuantity(int val)
    {
        ItemQuantity = val;


        if (ItemQuantity == 0)
        {
            Debug.Log("The item is gone");
            Icon.GetComponent<Image>().sprite = origSprite; 
            ItemData = null;

        }
    }

    public void AddQuantity()
    {
        ItemQuantity++;
    }

    public void Use()
    {
        //Add use function for item here
        if (ItemData)
            ItemData.Use();
    }
}
