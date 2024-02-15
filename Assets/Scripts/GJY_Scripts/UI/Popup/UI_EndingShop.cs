using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EndingShop : UI_Popup
{
    UI_Popup _alertPopup = null;

    enum Texts
    {
        Alert_Text,
    }

    enum Buttons
    {
        Exit_Btn,
    }

    enum InputFields
    {
        User_Input,
    }

    InputField input;
    Text alertText;

    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindInputField(typeof(InputFields));

        GetButton((int)Buttons.Exit_Btn).onClick.AddListener(ShowAndHideAlertPopup);

        input = GetInputField((int)InputFields.User_Input);
        alertText = GetText((int)Texts.Alert_Text);

        GetComponent<UI_InputActionHandler>().OnEscInvoke += ShowAndHideAlertPopup;
        GetComponent<UI_InputActionHandler>().OnEnterInvoke += InputCheck;
    }

    private void ShowAndHideAlertPopup()
    {
        if (_alertPopup == null)
            _alertPopup = Managers.UI.ShowPopupUI<UI_CanNotClosePcPopup>();
        else
            Managers.UI.ClosePopupUI(_alertPopup);
    }

    private void InputCheck()
    {
        string inputString = input.text;

        if (inputString == "오직 한효승만")
        {
            Managers.GameManager.day++;
            Managers.Scene.LoadScene(Define.Scenes.Ending);
        }
        else
        {
            alertText.text = "오직 한효승만.. 오직 한효승만.. 오직 한효승만.. 오직 한효승만.. 오직 한효승만.. 오직 한효승만.. 오직 한효승만.. ";
        }
    }
}
