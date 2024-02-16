using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMissile : IWeapon
{
    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        for(int i = 0; i< 10 + (playerStat.A_AddBullet * 2); i++)
        {
            Bullet_Missile missile = Managers.RM.Instantiate("Projectiles/Bullet_Missile").GetComponent<Bullet_Missile>();
            missile.Setup(origin, dir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration, playerStat.A_PierceCount);
        }        
    }
}
