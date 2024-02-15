using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scenes.Main;
        if (Managers.GameManager.day == 5)
            SoundManager.Instance.ChangeBackGroundMusic(Resources.Load<AudioClip>("Sounds/UI/creepy-hall"), 0.2f);
        else
            SoundManager.Instance.ChangeBackGroundMusic(Resources.Load<AudioClip>("Sounds/Dungoen/BGM_life_in_corrupted_binary"), 0.2f);
        Managers.UI.ShowSceneUI<UI_Room>();        
        Managers.GameManager.PCOn();
    }
}
