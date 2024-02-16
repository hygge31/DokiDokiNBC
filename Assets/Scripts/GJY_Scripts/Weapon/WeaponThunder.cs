using UnityEngine;
using System.Collections;

public class WeaponThunder : IWeapon
{
    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        for (int i = 0; i <= playerStat.A_AddBullet; i++)
        {
            Vector2 newOrigin = origin;
            if (i > 0)
            {
                // 랜덤한 범위 내에서 위치를 변경
                float offsetX = Random.Range(-4f, 4f);
                float offsetY = Random.Range(-4f, 4f);
                newOrigin += new Vector2(offsetX, offsetY);
            }

            Bullet_Thunder thunder = Managers.RM.Instantiate("Projectiles/Bullet_Thunder").GetComponent<Bullet_Thunder>();
            thunder.Setup(newOrigin, dir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration, playerStat.A_PierceCount);
        }
    }
}
