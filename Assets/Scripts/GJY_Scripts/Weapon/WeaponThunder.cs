using UnityEngine;
using System.Collections;

public class WeaponThunder : IWeapon
{
    private bool isRunning = false;

    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        Bullet_Thunder thunder = Managers.RM.Instantiate("Projectiles/Bullet_Thunder").GetComponent<Bullet_Thunder>();
        thunder.Setup(origin, dir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration, playerStat.A_PierceCount);
    }
}
