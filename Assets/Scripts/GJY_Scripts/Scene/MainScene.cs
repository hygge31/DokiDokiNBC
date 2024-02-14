using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.TestScenes.GJY;
        Managers.UI.ShowSceneUI<UI_Room>();
        Managers.Pool.Init();
    }
}
