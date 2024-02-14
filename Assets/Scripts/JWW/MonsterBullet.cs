using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    // 플레이어를 향해 날아가는 속도
    public float speed = 5f;

    // 플레이어와의 거리에 따른 퍼지는 정도
    public float spreadDistance = 1f;

    // 플레이어와의 방향 벡터
    private Transform target;
    public Vector3 playerDirection = Vector3.zero;
    public LayerMask targetMask;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }
    void Start()
    {
        transform.right = playerDirection;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        _rigidbody.velocity = playerDirection * speed;
        // 플레이어를 향해 이동
        //transform.Translate(playerDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Level 레이어와 충돌할 경우 제거
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            other.GetComponent<HealthSystem>().ChangeHealth(-1);
            Destroy(gameObject);
        }
    }
}