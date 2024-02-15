using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    UI_ShopAlertPopup _alertPopup = null;

    enum State
    {
        Main,
        Status,
        Upgrade,
        Skin,
        Boss,
    }

    enum Texts
    {
        Menu_Text,
        Command_Text,
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

    State currentState = State.Main;

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
        GetComponent<UI_InputActionHandler>().OnEnterInvoke += FocusInputField;
        GetComponent<UI_InputActionHandler>().OnEnterInvoke += ChangeState;
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
        
    }

    private void ChangeState()
    {
        alertText.color = Color.white;
        string inputCommand = input.text;
        Text commandText = GetText((int)Texts.Command_Text);

        if(input.text == "")
        {
            input.Select();
            input.ActivateInputField();
            alertText.text = "명령어를 입력해주세요.";
            return;
        }

        switch (currentState)
        {
            case State.Main:
                if (inputCommand == "상태")
                {
                    commandText.text = StatusCommands();
                }                    
                else if (inputCommand == "강화")
                {
                    commandText.text = UpgradeCommands();
                }                    
                else if (inputCommand == "코스튬")
                {
                    commandText.text = SkinCommands();
                }                    
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }                    
                break;
            case State.Status:
                if (inputCommand == "상태")
                {
                    commandText.text = StatusCommands();
                }
                else if (inputCommand == "강화")
                {
                    commandText.text = UpgradeCommands();
                }
                else if (inputCommand == "코스튬")
                {
                    commandText.text = UpgradeCommands();
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Upgrade:
                if (inputCommand == "상태")
                {
                    commandText.text = StatusCommands();
                }
                else if (inputCommand == "강화")
                {
                    commandText.text = UpgradeCommands();
                }
                else if (inputCommand == "코스튬")
                {
                    commandText.text = UpgradeCommands();
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Skin:
                if (inputCommand == "상태")
                {
                    commandText.text = StatusCommands();
                }
                else if (inputCommand == "강화")
                {
                    commandText.text = UpgradeCommands();
                }
                else if (inputCommand == "코스튬")
                {
                    commandText.text = UpgradeCommands();
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
        }

        input.text = "";
    }

    private string MainCommands()
    {
        return "명령어 목록\n" +
            "\n" +
            "[상태]\n[강화]\n[코스튬]";
    }

    private string StatusCommands()
    {
        return "번호를 입력해 강화 스탯 선택\n" +
            "[1] 공격력\n" +
            "[2] 공격속도\n" +
            "[3] 이동속도\n" +
            "[4] 관통 수\n" +
            "\n" +
            "[뒤로]";
    }

    private string UpgradeCommands()
    {
        return "번호를 입력해 강화 스탯 선택\n" +
            "[1] 공격력\n" +
            "[2] 공격속도\n" +
            "[3] 이동속도\n" +
            "[4] 관통 수\n" +
            "\n" +
            "[뒤로]";
    }

    private string SkinCommands()
    {
        return "번호를 입력해 강화 스탯 선택\n" +
            "[1] 공격력\n" +
            "[2] 공격속도\n" +
            "[3] 이동속도\n" +
            "[4] 관통 수\n" +
            "\n" +
            "[뒤로]";
    }
}
