using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scenes.Dungeon;
        Managers.UI.ShowSceneUI<UI_Hud>();
        Managers.Player.PlayerSetup();
        Managers.Attack.WeaponSetup();        
    }
}
