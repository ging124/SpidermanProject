using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem : Item
{
    public Sprite image;
    public GameEvent<Item> changeItem;
}
