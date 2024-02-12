using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : DropItem
{
    [Range(6, 7)][SerializeField] float _minPopPower;
    [Range(7, 8)][SerializeField] float _maxPopPower;
    [SerializeField] AnimationCurve _animCurve;

    protected override void Init()
    {
        base.Init();
    }

    public override void Setup(Transform spawnTransform = null)
    {
        base.Setup(spawnTransform);
        
        ItemDrop();
    }

    private void ItemDrop()
    {
        _coll.enabled = false;

        float randY = Random.Range(_minPopPower, _maxPopPower);
        float randX = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(randX, randY);
        _rigid.AddForce(dir, ForceMode2D.Impulse);

        StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        float current = 0;
        float startGrav = _rigid.gravityScale;
        

        while (current < 1)
        {
            current += Time.deltaTime;
            _rigid.gravityScale = Mathf.Lerp(startGrav, 0, _animCurve.Evaluate(current));

            yield return null;
        }

        _rigid.velocity = Vector2.zero;
        _coll.enabled = true;
    }

    protected override void InteractWithPlayer()
    {
        base.InteractWithPlayer();
        // To Do - 플레이어 체력 회복
        Clear();
    }

    protected override void Clear()
    {
        _rigid.gravityScale = 1;
        _rigid.velocity = Vector3.zero;        
    }
}
