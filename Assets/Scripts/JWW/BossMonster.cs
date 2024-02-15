using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class BossMonster : MonoBehaviour
{
    public GameObject bulletPrefab;//불릿
    public Transform firePoint;//발사 트랜스폼
    private Transform target;//플레이어의 트랜스폼

    public float attackCooldown = 2f;//쿨다운
    private float nextAttackTime = 0f;//다음 공격 시간
    private float bulletSpeed = 5f;

    [SerializeField]
    public float bossHealth = 100f; // 보스의 최대 체력
    private float maxHealth = 100f;
    private float healthPercentage;//보스 퍼센테이지

    private void Awake()
    {
        bossHealth = maxHealth;
        nextAttackTime = Time.time;
        target = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        RotateFirePoint();
        healthPercentage = (bossHealth / 100f) * 100f;

        if (Time.time >= nextAttackTime)
        {
            if (healthPercentage >= 70f)
            {
                ExecuteSingleAttack();
            }
            else if (healthPercentage >= 40f && healthPercentage < 70f)
            {
                attackCooldown = 3f;
                ExecuteDoubleAttack();
            }
            else if (healthPercentage >= 20f && healthPercentage < 40f)
            {
                attackCooldown = 5f;
                ExecuteTripleAttack();
            }
            else
            {
                attackCooldown = 7f;
                ExecuteQuadrupleAttack();
            }

            nextAttackTime = Time.time + attackCooldown;
        }
    }

    private void ExecuteQuadrupleAttack()
    {
        ExecuteRandomAttack(3,4);
        Invoke("ExecuteTripleAttack",attackCooldown/6);
    }

    private void ExecuteTripleAttack()
    {
        ExecuteRandomAttack(4, 6);
        Invoke("ExecuteDoubleAttack", attackCooldown/4);
    }

    private void ExecuteDoubleAttack()
    {
        ExecuteRandomAttack(3, 4);
        Invoke("ExecuteSingleAttack", attackCooldown/5);
    }

    private void ExecuteSingleAttack()
    {
        ExecuteRandomAttack(1, 3);//1 ~ 2
    }

    void ExecuteRandomAttack(int min,int max)//랜덤 공격
    {
        min = Mathf.Clamp(min,1, 6);
        max = Mathf.Clamp(max,1, 6);
        int attackPattern = Random.Range(min, max); // 1부터 5까지의 무작위 패턴 선택

        switch (attackPattern)
        {
            case 1:
                FireStraight();
                break;
            case 2:
                FireSpread();
                break;
            case 3:
                FireCircle();
                break;
            case 4:
                FireDiverge();
                break;
            case 5:
                FireRandomDirection();
                break;
            default:
                Debug.Log("잘못된 공격 패턴입니다.");
                break;
        }
    }

    void FireStraight()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        MonsterBullet newbullt = bullet.GetComponent<MonsterBullet>();
        newbullt.speed = bulletSpeed;
    }

    void FireSpread()
    {
        for (int i = -2; i <= 2; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, i * 15f));
            MonsterBullet newbullt = bullet.GetComponent<MonsterBullet>();
            newbullt.speed = bulletSpeed;
        }
    }

    void FireCircle()
    {
        for (int i = 0; i < 360; i += 20)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, i));
            MonsterBullet newbullt = bullet.GetComponent<MonsterBullet>();
            newbullt.speed = bulletSpeed;
        }
    }
    void FireDiverge()
    {
        for (int i = -1; i <= 0; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, i * 30f + 15f));
            MonsterBullet newbullt = bullet.GetComponent<MonsterBullet>();
            newbullt.speed = bulletSpeed;
            newbullt.SpreadBulletInOnePoint();
        }
    }

    void FireRandomDirection()
    {
        for (int i = 0; i < 5; i++)
        {
            float randomAngle = Random.Range(0f, 360f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, randomAngle));
            MonsterBullet newbullt = bullet.GetComponent<MonsterBullet>();
            newbullt.speed = bulletSpeed;
            newbullt.TrackingBullet();
        }
    }
    public void RotateFirePoint()//발사 위치와 발사 회전을 플레이어쪽으로 수정.
    {
        firePoint.localPosition = (target.transform.position- transform.position).normalized; //보스에서 플레이어를 향한 단위 벡터만큼 떨어진 곳
        firePoint.right = (target.transform.position - transform.position).normalized;//발사 위치의 오른쪽을 플레이어 방향으로 설정
    }
}