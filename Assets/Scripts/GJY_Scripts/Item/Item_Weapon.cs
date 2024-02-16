using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Weapon : DropItem
{
    protected override void Init()
    {
        base.Init();
        
    }

    protected override void InteractWithPlayer()
    {
        base.InteractWithPlayer();

        // To Do - 플레이어 무기 교체 및 UI 연동        
        Managers.Attack.SetWeapon(_itemSO);
    }

    public override void Setup(Item_SO item, Transform spawnTransform = null)
    {
        base.Setup(spawnTransform);

        _itemSO = item;
        _rend.sprite = _itemSO.sprite;
    }
}
