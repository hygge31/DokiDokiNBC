using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    UI_ShopAlertPopup _alertPopup = null;

    enum Texts
    {
        Menu_Text,
        Command_Text
    }

    enum Buttons
    {
        Exit_Btn,
    }

    enum InputFields
    {
        User_Input,
    }

    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindInputField(typeof(InputFields));

        GetButton((int)Buttons.Exit_Btn).onClick.AddListener(ShowAndHideAlertPopup);

        GetComponent<UI_InputActionHandler>().OnEscInvoke += ShowAndHideAlertPopup;
        GetComponent<UI_InputActionHandler>().OnEnterInvoke += FocusInputField;        
    }

    private void ShowAndHideAlertPopup()
    {
        if (_alertPopup == null)
            _alertPopup = Managers.UI.ShowPopupUI<UI_ShopAlertPopup>();
        else
            Managers.UI.ClosePopupUI(_alertPopup);
    }

    private void FocusInputField()
    {
        InputField input = GetInputField((int)InputFields.User_Input);
        input.Select();
        input.ActivateInputField();
    }
}
