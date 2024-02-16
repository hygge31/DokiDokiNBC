using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNormal : IWeapon
{
    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        float initialAngle = -15f * (playerStat.A_AddBullet / 2f);

        if(playerStat.A_AddBullet == 0)
        {
            // 한 발만 발사할 때
            Bullet_Fireball onefireball = Managers.RM.Instantiate("Projectiles/Bullet_Fireball").GetComponent<Bullet_Fireball>();
            onefireball.Setup(origin, dir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration, playerStat.A_PierceCount);
        }
        

        // 한 발 이상 발사
        for (int i = 0; i <= playerStat.A_AddBullet; i++)
        {
            // 투사체 간 거리 각도 계산
            float angle = initialAngle + i * 15f;

            // 회전된 방향으로 투사체를 생성합니다.
            Vector2 newDir = RotateVector2(dir, angle);
            Bullet_Fireball fireball = Managers.RM.Instantiate("Projectiles/Bullet_Fireball").GetComponent<Bullet_Fireball>();
            fireball.Setup(origin, newDir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration, playerStat.A_PierceCount);
        }
    }

    // 벡터를 회전하는 메서드입니다.
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}