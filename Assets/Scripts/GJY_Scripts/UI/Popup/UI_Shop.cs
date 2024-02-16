using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
    UI_Popup _alertPopup = null;

    private UI_Popup currentShowMenu = null;
    private PlayerStatManager playerStatManager = null;

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

    enum Images
    {
        Blind,
    }

    State currentState = State.Main;

    InputField input;
    Text alertText;

    protected override void Init()
    {
        base.Init();

        if(Managers.GameManager.day == 4)
            currentState = State.Boss;        

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));
        BindInputField(typeof(InputFields));
        BindImage(typeof(Images));

        GetButton((int)Buttons.Exit_Btn).onClick.AddListener(ShowAndHideAlertPopup);

        input = GetInputField((int)InputFields.User_Input);
        alertText = GetText((int)Texts.Alert_Text);

        GetComponent<UI_InputActionHandler>().OnEscInvoke += ShowAndHideAlertPopup;
        GetComponent<UI_InputActionHandler>().OnEnterInvoke += ChangeState;

        ChangeState();

        playerStatManager = Managers.Player;
    }

    private void ShowAndHideAlertPopup()
    {
        if (_alertPopup == null)
        {
            switch (currentState)
            {
                case State.Boss:                                    
                    _alertPopup = Managers.UI.ShowPopupUI<UI_CanNotClosePcPopup>();
                    break;
                default:
                    _alertPopup = Managers.UI.ShowPopupUI<UI_ShopAlertPopup>();
                    break;
            }            
        }
        else
        {
            Managers.UI.ClosePopupUI(_alertPopup);
        }            
    }

    // 만든 사람이 봐도 마음에 안드네~~
    private void ChangeState()
    {
        alertText.color = Color.white;
        string inputCommand = input.text;
        Text commandText = GetText((int)Texts.Command_Text);
        Text menuText = GetText((int)Texts.Menu_Text);

        if(Managers.GameManager.day == 4)
            commandText.text = MainCommands();

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
                    BackToMainShop(commandText, menuText);
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
                    BackToMainShop(commandText, menuText);
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Skin:                
                if (inputCommand == "뒤로")
                {
                    BackToMainShop(commandText, menuText);
                }
                else
                {
                    alertText.text = "잘못된 입력입니다.";
                    alertText.color = Color.red;
                }
                break;
            case State.Boss:
                if (inputCommand == "과제 제출")
                {
                    alertText.text = "";
                    StartCoroutine(ChangeSceneRoutine());                    
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

    private IEnumerator ChangeSceneRoutine()
    {
        float current = 0;
        float percent = 0;

        Image image = GetImage((int)Images.Blind);
        Color color = image.color;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 2;

            color.a = Mathf.Lerp(0, 1, percent);
            image.color = color;

            yield return null;
        }        

        Managers.Scene.LoadScene(Define.Scenes.Dungeon);
    }

    private void BackToMainShop(Text command, Text menu)
    {
        alertText.text = "";
        command.text = MainCommands();
        menu.text = MenuName("메인");
        if (currentShowMenu != null)
            Managers.UI.ClosePopupUI(currentShowMenu);
        currentState = State.Main;
    }

    private void ApplyUpgrade(int transform, string input)
    {
        if(Managers.Player.Upgrade(input) == false)
        {
            alertText.text = "조각이 부족합니다.";
            alertText.color = Color.red;
            return;
        }

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
        if(Managers.GameManager.day == 4)
        {
            return "명령어 목록\n" +
            "\n" +
            "[과제 제출]";
        }

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
            "[1] 공격력    -  50 조각\n" +
            "[2] 공격속도  -  25 조각\n" +
            "[3] 이동속도  -  100 조각\n" +
            "\n" +
            "[뒤로]";
    }

    private string SkinCommands()
    {
        return "명령어 목록\n" +
            "[뒤로]";
    }
}
