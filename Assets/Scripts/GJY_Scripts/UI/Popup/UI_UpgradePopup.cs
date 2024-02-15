using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradePopup : UI_Popup
{
    public enum UpgradeTransforms
    {
        Atk_Images,
        FireRate_Images,
        MoveSpeed_Images,        
    }

    enum Texts
    {
        RemainCode_Text,
    }

    protected override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(UpgradeTransforms));
        BindText(typeof(Texts));

        GetText((int)Texts.RemainCode_Text).text = $"남은 코드 조각 : {Managers.GameManager.CodePiece}";
    }

    public void LoadSubImages(int atk, int fireRate, int moveSpd)
    {
        for (int i = 0; i < atk; i++)
        {
            Managers.UI.MakeSub<UI_Upgrade>(Get<Transform>((int)UpgradeTransforms.Atk_Images)).GetComponent<Image>();
        }
        for (int i = 0; i < fireRate; i++)
        {
            Managers.UI.MakeSub<UI_Upgrade>(Get<Transform>((int)UpgradeTransforms.FireRate_Images)).GetComponent<Image>();
        }
        for (int i = 0; i < moveSpd; i++)
        {
            Managers.UI.MakeSub<UI_Upgrade>(Get<Transform>((int)UpgradeTransforms.MoveSpeed_Images)).GetComponent<Image>();
        }
    }

    public Image MakeSubImage(int index)
    {
        GetText((int)Texts.RemainCode_Text).text = $"남은 코드 조각 : {Managers.GameManager.CodePiece}";
        return Managers.UI.MakeSub<UI_Upgrade>(Get<Transform>(index)).GetComponent<Image>();
    }
}
