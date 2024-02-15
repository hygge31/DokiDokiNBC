using UnityEngine;
using System.Collections;

public class WeaponThunder : IWeapon
{
    private bool isRunning = false;

    public void Attack(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        if (!isRunning)
        {
            RunCodeWithDelay(playerStat, origin, dir);
        }
    }

    IEnumerator RunCodeWithDelay(PlayerStatManager playerStat, Vector2 origin, Vector2 dir)
    {
        isRunning = true;

        for (int i = 0; i < 3; i++)
        {
            Bullet_Thunder thunder = Managers.RM.Instantiate("Projectiles/Bullet_Thunder").GetComponent<Bullet_Thunder>();
            thunder.Setup(origin, dir, playerStat.A_Atk, playerStat.W_BulletSpeed, playerStat.W_Duration);
            // 1초 대기
            yield return new WaitForSeconds(1.0f);
        }

        // 다시 for 루프 실행
        isRunning = false;
    }
}
