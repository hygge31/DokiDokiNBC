using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

public class BossMonster : MonoBehaviour
{
    // 공격 패턴 종류를 나타내는 열거형
    public enum AttackPattern
    {
        StraightShot,
        CircleShot,
        RandomMovement
    }
    public GameObject firePosition;
    public GameObject bulletPrefab; // 총알 프리팹

    private Transform target;
    // 현재 공격 패턴
    private AttackPattern currentPattern;

    // 다음 공격 패턴까지의 대기 시간
    public float timeBetweenPatterns = 5f;
    private float patternTimer;

    [Header("일반 직선 공격 정보")]
    private float nextAttackTime;
    public float attackInterval = 0.3f;

    [Header("원형 공격 정보")]
    // 원형 공격의 반지름
    public float radius = 3f;

    // 원형 공격에 사용될 총알 수
    public int bulletCount = 10;

    // 원형 공격이 완료될 때까지 걸리는 시간
    public float duration = 5f;

    // 원형 공격의 회전 속도
    public float rotationSpeed = 50f;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        // 초기 공격 패턴 설정
        currentPattern = AttackPattern.StraightShot;
        patternTimer = timeBetweenPatterns;

        // 보스 몬스터 공격 패턴 시작
        StartCoroutine(StraightShotPattern());
    }

    void Update()
    {
        LookPlayer();
        // 다음 공격 패턴까지의 시간 업데이트
        patternTimer -= Time.deltaTime;
        if (patternTimer <= 0)
        {
            // 다음 공격 패턴 실행
            StartCoroutine(StartAttackPattern());
            patternTimer = timeBetweenPatterns;
        }
    }

    IEnumerator StartAttackPattern()
    {
        // 다음 공격 패턴을 랜덤하게 선택
        //currentPattern = (AttackPattern)Random.Range(0, System.Enum.GetValues(typeof(AttackPattern)).Length);
        currentPattern = AttackPattern.CircleShot;
        // 선택된 공격 패턴 실행
        switch (currentPattern)
        {
            //case AttackPattern.StraightShot:
            //    StartCoroutine(StraightShotPattern());
            //    break;
            case AttackPattern.CircleShot:
                StartCoroutine(CircleShotPattern());
                break;
            case AttackPattern.RandomMovement:
                StartCoroutine(RandomMovementPattern());
                break;
            default:
                break;
        }

        yield return null;
    }

    IEnumerator StraightShotPattern()
    {
        while (true)
        {
            // 현재 시간이 다음 공격 시간보다 크거나 같은 경우 공격 실행
            if (Time.time >= nextAttackTime)
            {
                // Bullet 프리팹을 생성하여 플레이어를 향해 공격
                GameObject bullet = Instantiate(bulletPrefab, firePosition.transform.position, Quaternion.identity);
                // Bullet이 플레이어를 향하도록 설정
                bullet.GetComponent<MonsterBullet>().playerDirection = (target.transform.position - transform.position).normalized;

                // 다음 공격 시간 설정
                nextAttackTime = Time.time + attackInterval;
            }

            yield return null;
        }
    }

    IEnumerator CircleShotPattern()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 원형 공격을 하는 동안 프레임당 각도 계산
            float angleStep = 360f / bulletCount;

            // 각도를 기준으로 Bullet 프리팹을 생성하여 플레이어를 향해 공격
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = i * angleStep;
                Vector3 spawnPosition = transform.position + Quaternion.Euler(0, 0, angle) * (Vector3.up * radius);

                GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
                bullet.GetComponent<MonsterBullet>().playerDirection = (target.transform.position - spawnPosition).normalized;
            }

            // 다음 프레임까지 대기
            yield return null;

            // 다음 프레임까지의 경과 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 원형 공격 동안 보스 몬스터를 회전시킴
            //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        // 원형 공격 종료 후 스크립트 제거
        Destroy(gameObject);

        yield return null;
    }

    IEnumerator RandomMovementPattern()
    {
        // 무작위 이동 공격 패턴 구현
        Debug.Log("Random Movement Pattern Executed");

        yield return null;
    }
    public void LookPlayer()
    {
        firePosition.transform.localPosition = (target.transform.position- transform.position).normalized ;
    }
}