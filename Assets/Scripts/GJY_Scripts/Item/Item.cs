using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected Item_SO _itemSO;

    private SpriteRenderer _rend;

    private void Awake()
    {        
        Init();        
    }

    protected virtual void Init() 
    {
        _rend = GetComponent<SpriteRenderer>();
        _rend.sprite = _itemSO.sprite;
    }

    public virtual void Setup() { }
}
