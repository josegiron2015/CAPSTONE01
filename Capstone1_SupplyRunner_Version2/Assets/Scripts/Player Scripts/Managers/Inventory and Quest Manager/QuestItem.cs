using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Item", menuName = "Item/Quest Item")]
public class QuestItem : Item {


    public override void Use()
    {
        Debug.Log("This is a quest item and is not useable");

        Inventory.Instance.RemoveItem(this);
        return;
    }
}
