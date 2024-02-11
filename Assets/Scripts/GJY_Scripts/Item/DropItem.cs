using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : Item
{
    protected Rigidbody2D _rigid;
    protected Collider2D _coll;

    protected override void Init()
    {
        base.Init();

        _rigid = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        InteractWithPlayer();
    }

    protected virtual void InteractWithPlayer() 
    {
        gameObject.SetActive(false);
    }

    protected virtual void Clear() { }
}
