using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager
{
    public Action<Item_SO> OnWeaponSetup;
    public Action<Item_SO> OnChangeWeapon;

    private IWeapon currentWeapon;
    private Item_SO currentSO;

    public void SetWeapon(Item_SO item) // 무기변경
    {
        Define.Weapons weaponType = item.weaponType;

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
        currentSO = Resources.Load<Item_SO>("Scriptable/Weapon_Normal"); ;
    }

    public void WeaponSetup()
    {
        OnWeaponSetup?.Invoke(currentSO);
    }

    public void UseWeapon(Vector2 origin, Vector2 dir)
    {
        currentWeapon.Attack(origin, dir);
    }
}
