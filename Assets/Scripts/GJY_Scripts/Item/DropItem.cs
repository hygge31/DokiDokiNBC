using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : Item
{
    protected Rigidbody2D _rigid;
    protected Collider2D _coll;

    private Poolable _poolable;

    protected override void Init()
    {
        base.Init();

        _rigid = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
        _poolable = GetComponent<Poolable>();
    }

    public override void Setup(Transform spawnTransform = null)
    {
        base.Setup(spawnTransform);

        transform.position = spawnTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        InteractWithPlayer();
    }

    protected virtual void InteractWithPlayer() 
    {
        Managers.Pool.Push(_poolable);
    }

    protected virtual void Clear() { }
}
