using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager
{
    public Action<Item_SO> OnWeaponSetup;
    public Action<Item_SO> OnChangeWeapon;

    private IWeapon currentWeapon;
    public Item_SO CurrentSO { get; private set; }

    private PlayerStatManager playerStatManager;

    public void SetWeapon(Item_SO item) // 무기변경
    {
        Define.Weapons weaponType = item.weaponType;
        CurrentSO = item;

        switch (weaponType)
        {
            case Define.Weapons.Weapon_Normal:
                currentWeapon = new WeaponNormal();
                break;
            case Define.Weapons.Weapon_Missile:
                currentWeapon = new WeaponMissile();
                break;
            case Define.Weapons.Weapon_Turret:
                currentWeapon = new WeaponTurret();
                break;
        }

        OnChangeWeapon?.Invoke(item);
    }

    public void Init()
    {
        currentWeapon = new WeaponNormal();
        CurrentSO = Resources.Load<Item_SO>("Scriptable/Weapon_Normal");        
        playerStatManager = Managers.Player;
        OnWeaponSetup?.Invoke(CurrentSO);
    }

    public void WeaponSetup()
    {
        OnWeaponSetup?.Invoke(CurrentSO);
    }

    public void UseWeapon(Vector2 origin, Vector2 dir)
    {
        currentWeapon.Attack(playerStatManager, origin, dir);
    }

    public void Clear()
    {
        OnWeaponSetup = null;
        OnChangeWeapon = null;
    }
}
