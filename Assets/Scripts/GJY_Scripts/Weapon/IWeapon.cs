using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir);
}
