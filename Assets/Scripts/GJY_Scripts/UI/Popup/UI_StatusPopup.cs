using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatusPopup : UI_Popup
{
    enum Texts
    {
        Stauts_Text,
    }

    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));

        GetText((int)Texts.Stauts_Text).text = UpdateStatus();
    }

    private string UpdateStatus()
    {
        PlayerStatManager stat = Managers.Player;

        string showStats = $"상태\n" +
            $"\n" +
            $"체력 : {stat.Hp}\n" +
            $"무기 : {Managers.Attack.CurrentSO.displayName}\n" +
            $"공격력 : {stat.A_Atk}\n" +
            $"공격 속도 : {stat.A_FireRate:F2}\n" +
            $"이동 속도 : {stat.A_MoveSpeed}\n" +
            $"다중 사격 : {stat.A_AddBullet}\n" +
            $"관통 : {stat.A_AddBullet}\n";

        return showStats;
    }
}
