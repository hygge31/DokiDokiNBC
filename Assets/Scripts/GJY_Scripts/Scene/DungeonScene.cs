using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.TestScenes.KJH;
        Managers.Player.PlayerSetup();
        Managers.Attack.WeaponSetup();
        Managers.UI.ShowSceneUI<UI_Hud>();
    }
}
