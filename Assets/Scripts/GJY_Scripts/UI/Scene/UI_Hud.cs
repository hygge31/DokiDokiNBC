using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    private Stack<Image> _hpImages = new Stack<Image>();

    enum Transforms
    {
        HPImages_Panel,
        Perks_Panel,
    }

    protected override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(Transforms));

        Managers.Player.OnApplyPerkStat += GetPerk;
    }

    private void GetPerk(Item_SO item)
    {
        UI_Perk perkUI = Managers.UI.MakeSub<UI_Perk>(Get<Transform>((int)Transforms.Perks_Panel));
        perkUI.Setup(item);
    }
}
