using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scenes.Title;

        Managers.Player.Init();
        Managers.Attack.Init();
        Managers.Pool.Init();

        Managers.UI.ShowSceneUI<UI_Title>();
    }
}
