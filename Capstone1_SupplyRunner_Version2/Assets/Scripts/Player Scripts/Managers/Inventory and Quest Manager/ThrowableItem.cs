using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Throwable Item", menuName = "Item/Throwable Item")]
public class ThrowableItem : Item {
    

    public override void Use()
    {
        Inventory.Instance.RemoveItem(this);
        base.Use();
    }
}
