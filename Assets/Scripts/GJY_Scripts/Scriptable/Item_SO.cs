using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item_")]
public class Item_SO : ScriptableObject
{
    public Sprite sprite;

    public string displayName;
    [TextArea]
    public string displayDesc;

    public bool isActive;
    public bool isStackable;
}
