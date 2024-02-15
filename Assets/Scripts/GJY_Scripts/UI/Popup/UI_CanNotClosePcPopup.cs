using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanNotClosePcPopup : UI_Popup
{
    enum Texts
    {
        ExitWarning_Text,
        Close_Text,
    }

    enum Buttons
    {
        Yes_Btn,
    }

    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.Yes_Btn).onClick.AddListener(ClosePopup);

        if (Managers.GameManager.day == 5)
            OlnyHan();
    }

    private void OlnyHan()
    {
        Text warning = GetText((int)Texts.ExitWarning_Text);
        warning.text = "오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..";
        warning.color = Color.red;

        Text close = GetText((int)Texts.Close_Text);
        close.text = "오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..오직 한효승만..";
        close.color = Color.red;
    }
}
