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

    protected override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(UpgradeTransforms));
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
        return Managers.UI.MakeSub<UI_Upgrade>(Get<Transform>(index)).GetComponent<Image>();
    }
}
