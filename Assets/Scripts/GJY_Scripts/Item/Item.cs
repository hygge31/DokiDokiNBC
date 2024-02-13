using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected Item_SO _itemSO;

    protected SpriteRenderer _rend;

    private void Awake()
    {        
        Init();        
    }

    protected virtual void Init() 
    {
        _rend = GetComponent<SpriteRenderer>();        
    }

    public virtual void Setup(Transform spawnTransform = null) { }
    public virtual void Setup(Item_SO item, Transform spawnTrasnform = null) { }
}
