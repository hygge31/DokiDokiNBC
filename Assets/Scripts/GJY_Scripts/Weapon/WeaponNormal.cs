using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNormal : IWeapon
{
    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {        
        Bullet_Fireball fireball = Managers.RM.Instantiate("Projectiles/Bullet_Fireball").GetComponent<Bullet_Fireball>();
        fireball.Setup(origin, dir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration);
    }
}
