using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMissile : IWeapon
{
    public void Attack(Vector2 origin, Vector2 dir)
    {
        for(int i = 0; i< 10; i++)
        {
            Bullet_Missile missile = Managers.RM.Instantiate("Projectiles/Bullet_Missile").GetComponent<Bullet_Missile>();
            missile.Setup(origin, 2, 10, 5);
        }        
    }
}
