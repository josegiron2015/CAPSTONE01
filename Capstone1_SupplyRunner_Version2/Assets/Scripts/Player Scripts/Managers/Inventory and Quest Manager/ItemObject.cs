using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    public Item ItemData;

    public void Update()
    {

        
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Input.GetButtonDown("Square"))
        {
            //if (Input.GetKeyDown(KeyCode.Space))
                //PickUp(Inventory.Instance);         

            if (Inventory.Instance.IsItemExist("New Item"))
                Debug.Log("New item exists in inventory");
            
        }
    }

    public void AddItem()
    {
        PickUp(Inventory.Instance);
        Debug.Log("Item has been added in Inventory");
        //this.gameObject.GetComponent<GlowObjectCmd>().LerpFactor = 0;
    }


    public void PickUp(Inventory inventory)
    {
        if (ItemData)
            inventory.AddItem(ItemData);
        else
            Debug.Log("No item data exists");
    }
}
