using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNormal : IWeapon
{
    public void Attack(Vector2 origin, Vector2 dir)
    {
        Debug.Log("일반 공격");
        Bullet_Fireball fireball = Managers.RM.Instantiate("Projectiles/Bullet_Fireball").GetComponent<Bullet_Fireball>();
        fireball.Setup(origin, dir, 1f, 6f, 5f);
    }
}
