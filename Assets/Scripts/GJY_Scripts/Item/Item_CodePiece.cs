using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_CodePiece : DropItem
{    
    [SerializeField] float _speed;
    [SerializeField] float _increaseSpd;

    private Transform _target;

    private float _currentSpd;
    private bool _magneting = false;    

    protected override void Init()
    {
        base.Init();
        
        _target = FindObjectOfType<GJY_PlayerTemp>().transform;        
    }

    private void FixedUpdate()
    {
        if (_magneting == false)
            return;

        Vector3 targetDir = (_target.position - transform.position).normalized;        
        
        Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, targetDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _currentSpd * Time.deltaTime);

        _currentSpd += _increaseSpd;
        _rigid.velocity = transform.up * _currentSpd;        
    }

    public override void Setup(Transform spawnTransform = null)
    {
        base.Setup(spawnTransform);
        
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        _currentSpd = _speed;
        StartCoroutine(DropAndMagneting());
    }

    private IEnumerator DropAndMagneting()
    {
        _rigid.velocity = transform.up * _speed;
        Vector2 startSpd = _rigid.velocity;
        Vector2 endSpd = transform.up;

        float current = 0;
        float percent = 0;
        float time = 0.5f;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            _rigid.velocity = Vector2.Lerp(startSpd, endSpd, percent);

            yield return null;
        }

        _rigid.velocity = endSpd;
        _magneting = true;
    }

    protected override void InteractWithPlayer()
    {        
        base.InteractWithPlayer();

        // To Do - 아이템이 닿으면 재화가 올라가는 Action 호출
        Clear();
    }

    protected override void Clear()
    {
        _currentSpd = _speed;
        _rigid.velocity = Vector3.zero;
        _magneting = false;
    }
}
