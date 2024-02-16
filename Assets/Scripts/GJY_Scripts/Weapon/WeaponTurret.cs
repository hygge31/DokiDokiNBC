using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTurret : IWeapon
{
    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        Debug.Log("터렛 생성");
    }
}
