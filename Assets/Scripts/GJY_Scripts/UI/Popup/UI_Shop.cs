using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    UI_ShopAlertPopup _alertPopup = null;

    private UI_Popup currentShowMenu = null;
    private PlayerStatManager playerStatManager = null;

    enum State
    {
        Main,
        Status,
        Upgrade,
        Skin,
        Boss,
        Ending,
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

        playerStatManager = Managers.Player;
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

    // 만든 사람이 봐도 마음에 안드네~~
    private void ChangeState()
    {
        alertText.color = Color.white;
        string inputCommand = input.text;
        Text commandText = GetText((int)Texts.Command_Text);
        Text menuText = GetText((int)Texts.Menu_Text);

        if (input.text == "")
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
                    alertText.text = "";
                    commandText.text = StatusCommands();
                    menuText.text = MenuName("상태");
                    currentShowMenu = Managers.UI.ShowPopupUI<UI_StatusPopup>();
                    currentState = State.Status;
                }
                else if (inputCommand == "강화")
                {
                    alertText.text = "";
                    commandText.text = UpgradeCommands();
                    menuText.text = MenuName("강화");
                    UI_UpgradePopup upMenu = Managers.UI.ShowPopupUI<UI_UpgradePopup>();
                    upMenu.LoadSubImages(playerStatManager.upgrade_Atk, playerStatManager.upgrade_FireRate, playerStatManager.upgrade_MoveSpeed);
                    currentShowMenu = upMenu;
                    currentState = State.Upgrade;
                }
                else if (inputCommand == "코스튬")
                {
                    alertText.text = "";
                    commandText.text = SkinCommands();
                    menuText.text = MenuName("코스튬");
                    // 코스튬 팝업
                    currentState = State.Skin;
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Status:
                if (inputCommand == "뒤로")
                {
                    alertText.text = "";
                    commandText.text = MainCommands();
                    menuText.text = MenuName("메인");
                    Managers.UI.ClosePopupUI(currentShowMenu);
                    currentState = State.Main;
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Upgrade:
                if (inputCommand == "1")
                {
                    alertText.text = "";
                    if (playerStatManager.upgrade_Atk == 5)
                        break;

                    ApplyUpgrade((int)UI_UpgradePopup.UpgradeTransforms.Atk_Images, inputCommand);                    
                }
                else if (inputCommand == "2")
                {
                    alertText.text = "";
                    if (playerStatManager.upgrade_FireRate == 5)
                        break;

                    ApplyUpgrade((int)UI_UpgradePopup.UpgradeTransforms.FireRate_Images, inputCommand);
                }
                else if (inputCommand == "3")
                {
                    alertText.text = "";
                    if (playerStatManager.upgrade_MoveSpeed == 5)
                        break;

                    ApplyUpgrade((int)UI_UpgradePopup.UpgradeTransforms.MoveSpeed_Images, inputCommand);
                }
                else if (inputCommand == "뒤로")
                {
                    alertText.text = "";
                    commandText.text = MainCommands();
                    menuText.text = MenuName("메인");
                    Managers.UI.ClosePopupUI(currentShowMenu);
                    currentState = State.Main;
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
                    alertText.text = "";
                }
                else if (inputCommand == "강화")
                {
                    alertText.text = "";
                }
                else if (inputCommand == "코스튬")
                {
                    alertText.text = "";
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Boss:
                if (inputCommand == "과제")
                {
                    alertText.text = "";
                }
                else if (inputCommand == "강화")
                {
                    alertText.text = "";
                }
                else if (inputCommand == "코스튬")
                {
                    alertText.text = "";
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Ending:
                if (inputCommand == "오직 한효승만")
                {

                }
                else
                {
                    alertText.text = "오직 한효승만..";
                    alertText.color = Color.red;
                }
                break;
        }

        input.text = "";
    }

    private void ApplyUpgrade(int transform, string input)
    {
        Managers.Player.Upgrade(input);

        UI_UpgradePopup upMenu = currentShowMenu as UI_UpgradePopup;
        if (upMenu != null)
            upMenu.MakeSubImage(transform);
    }

    private string MenuName(string name)
    {
        string text = $"현재 메뉴\n\n  [{name}]";

        return text;
    }

    private string MainCommands()
    {
        return "명령어 목록\n" +
            "\n" +
            "[상태]\n[강화]\n[코스튬]";
    }

    private string StatusCommands()
    {
        return "명령어 목록\n" +
            "\n" +
            "[뒤로]";
    }

    private string UpgradeCommands()
    {
        return "명령어 목록\n" +
            "\n" +
            "번호를 입력해 강화 스탯 선택\n" +
            "[1] 공격력\n" +
            "[2] 공격속도\n" +
            "[3] 이동속도\n" +
            "\n" +
            "[뒤로]";
    }

    private string SkinCommands()
    {
        return "명령어 목록\n" +
            "[뒤로]";
    }
}
