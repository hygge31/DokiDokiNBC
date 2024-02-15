using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Missile : Bullet
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] AnimationCurve curve;

    private float _atk;
    private float _speed;
    private float _activeTime;
    private int _pierceCount;

    private bool _isLockOn = false;    

    private Rigidbody2D _rigid;
    private Transform _target;

    private float currentActive;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        currentActive += Time.deltaTime;
        if(currentActive >= _activeTime)
            Clear();
    }

    private void FixedUpdate()
    {
        // To - Do 추적

        if (!_isLockOn)
            return;

        Tracking();
    }

    private void Tracking()
    {
        if (_target != null)
        {
            if (_target.gameObject.activeSelf)
            {
                Vector3 targetDir = (_target.position - transform.position).normalized;

                Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, targetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _speed * Time.deltaTime);

                _rigid.velocity = transform.up * _speed;
            }
            else
            {
                if (_target != null)
                    _target = null;
                _rigid.velocity = transform.up * _speed;
            }
        }
        else
        {
            _rigid.velocity = transform.up * _speed;
        }
    }

    public override void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration)
    {
        base.Setup(spawnPos, dir, atk, travelSpeed, duration);

        currentActive = 0;

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.position = spawnPos + transform.up * 0.5f;

        _atk = atk;
        _speed = travelSpeed;
        _activeTime = duration;
        //_pierceCount = pierceCount;

        StartCoroutine(LockOnTargetRoutine());
    }

    private IEnumerator LockOnTargetRoutine()
    {
        float current = 0;
        float percent = 0;

        Vector2 startSpeed = _rigid.velocity;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 1f;

            _rigid.velocity = Vector2.Lerp(startSpeed, transform.up * _speed, curve.Evaluate(percent));

            yield return null;
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 25, Vector2.zero, 0, targetLayer);
        if (hit)
            _target = hit.transform;

        _isLockOn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            // To - Do 적에게 데미지 및 사라지기
            Clear();
        }        
    }    

    public void Clear()
    {
        currentActive = 0;
        _isLockOn = false;
        _target = null;
        _rigid.velocity = Vector3.zero;
        transform.position = new Vector3(100, 0, 0);
        transform.rotation = Quaternion.identity;

        Managers.RM.Destroy(gameObject);
    }
}
