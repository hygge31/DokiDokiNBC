using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SoundManager.Instance.StopAudio();
        Managers.UI.ShowSceneUI<UI_Ending>();
    }
}
