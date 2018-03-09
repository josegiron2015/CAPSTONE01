using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Recovery,
    Quest,
    Throwable,
    Buff,
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Base Item")]
public class Item : ScriptableObject {

    public string ItemName;
    [TextArea]
    public string ItemDescription;
    public bool IsUseable;
    public Sprite ItemSprite;
    public ItemType Type;

    public Item()
    {

    }

    public virtual void Use()
    {   

    }
}
