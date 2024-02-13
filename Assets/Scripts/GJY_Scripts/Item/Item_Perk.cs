using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Perk : DropItem
{
    protected override void Init()
    {
        base.Init();
        
    }

    protected override void InteractWithPlayer()
    {
        base.InteractWithPlayer();

        // To Do - 플레이어 능력 부여

        UI_Perk perkUI = Managers.UI.MakeSub<UI_Perk>(Util.FindParentFromHUD("Perks_Panel"));
        perkUI.Setup(_itemSO);
    }

    public override void Setup(Item_SO item, Transform spawnTransform = null)
    {
        base.Setup(spawnTransform);

        _itemSO = item;
        _rend.sprite = _itemSO.sprite;
    }
}
