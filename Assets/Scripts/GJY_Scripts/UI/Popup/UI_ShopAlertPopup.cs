using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShopAlertPopup : UI_Popup
{
    enum Buttons
    {
        Yes_Btn,
        No_Btn1,
        No_Btn2,
    }

    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Yes_Btn).onClick.AddListener(YesBtn);
        GetButton((int)Buttons.No_Btn1).onClick.AddListener(ClosePopup);
        GetButton((int)Buttons.No_Btn2).onClick.AddListener(ClosePopup);
    }

    private void YesBtn()
    {
        Managers.GameManager.PCOff();
        Managers.UI.CloseAllPopupUI();
    }
}
