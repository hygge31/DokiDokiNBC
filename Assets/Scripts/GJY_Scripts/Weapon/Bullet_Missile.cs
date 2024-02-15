using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Missile : Bullet
{    
    [SerializeField] AnimationCurve curve;
    [SerializeField] LayerMask trackingLayer;

    [SerializeField] AudioClip fireClip;
    [SerializeField] AudioClip hitClip;

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
        if(currentActive >= Duration)
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
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, TravelSpeed * Time.deltaTime);

                _rigid.velocity = transform.up * TravelSpeed;
            }
            else
            {
                if (_target != null)
                    _target = null;
                _rigid.velocity = transform.up * TravelSpeed;
            }
        }
        else
        {
            _rigid.velocity = transform.up * TravelSpeed;
        }
    }

    public override void Setup(Vector3 spawnPos, Vector2 dir, float atk, float travelSpeed, float duration, int pierceCount)
    {
        base.Setup(spawnPos, dir, atk, travelSpeed, duration, pierceCount);

        currentActive = 0;

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.position = spawnPos + transform.up * 0.5f;

        SoundManager.Instance.PlayClip(fireClip);

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

            _rigid.velocity = Vector2.Lerp(startSpeed, transform.up * TravelSpeed, curve.Evaluate(percent));

            yield return null;
        }

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 25, Vector2.zero, 0, trackingLayer);
        if (hit)
            _target = hit.transform;

        _isLockOn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            HealthSystem healthSystem = collision.GetComponentInParent<HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.ChangeHealth(-Atk);
            }

            SoundManager.Instance.PlayClip(hitClip);

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
