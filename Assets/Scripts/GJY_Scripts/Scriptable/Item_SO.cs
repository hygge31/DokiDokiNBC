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

    public bool isWeapon;
    public bool isStackable;

    [Header("Stat")]    
    public float atk;
    public float fireRate;    
    public float moveSpeed;
    public float bulletSpeed;

    [Header("Ability")]
    public int addBullet;
    public int pierceCount;

    [Header("Weapon")]
    public Define.Weapons weaponType;

    [Header("Common_Weapon")]
    public float duration;
}
