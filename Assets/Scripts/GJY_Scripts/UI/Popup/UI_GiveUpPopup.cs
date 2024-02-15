using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GiveUpPopup : UI_Popup
{
    enum Images
    {
        Blind,
    }

    enum Buttons
    {
        Yes_Btn,
        No_Btn,
    }

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Yes_Btn).onClick.AddListener(() =>
        {
            Managers.Scene.LoadScene(Define.Scenes.Main);
            Managers.ResetGame();
        });
        GetButton((int)Buttons.No_Btn).onClick.AddListener(ClosePopup);
    }
}
