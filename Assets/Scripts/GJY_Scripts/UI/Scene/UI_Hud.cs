using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    private Stack<Image> _hpImages = new Stack<Image>();    
    private PlayerStatManager _playerStatManager;

    enum Transforms
    {
        HPImages_Panel,
        Perks_Panel,
    }

    enum Images
    {
        Weapon_Image,
    }

    enum Texts
    {
        CodePiece_Text,
        Atk_Text,
        FireRate_Text,
        MoveSpeed_Text,
        AddBullet_Text,
        Pierce_Text,
    }

    protected override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(Transforms));        
        BindImage(typeof(Images));
        BindText(typeof(Texts));

        Managers.Player.OnPlayerSetup += PlayerSetup;
        Managers.Player.OnPerkSetup += PerkSetup;
        Managers.Player.OnApplyPerkStat += GetPerk;
        Managers.Player.OnHealing += HPImageControl;
        Managers.Player.OnGetDamaged += HPImageControl;
        Managers.Player.OnWeaponChange += UpdateTexts;

        Managers.Attack.OnWeaponSetup += WeaponSetup;
        Managers.Attack.OnChangeWeapon += GetWeapon;

        Managers.GameManager.OnGetCodePiece += GetCodePiece;

        GetText((int)Texts.CodePiece_Text).text = $"{Managers.GameManager.CodePiece}";

        _playerStatManager = Managers.Player;
    }

    private void PlayerSetup()
    {
        int hp = Managers.Player.Hp;
        int count = hp / 2 + hp % 2;

        for (int i = 0; i < count; i++)
        {
            Image image = Managers.UI.MakeSub<UI_Hp>(Get<Transform>((int)Transforms.HPImages_Panel)).GetComponent<Image>();
            _hpImages.Push(image);
        }

        if (hp % 2 != 0)
        {
            _hpImages.Peek().fillAmount = 0.5f;
        }

        UpdateTexts();
    }

    private void PerkSetup(string perk, int count)
    {
        Item_SO item = Resources.Load<Item_SO>($"Scriptable/{perk}");
        UI_Perk perkUI = Managers.UI.MakeSub<UI_Perk>(Get<Transform>((int)Transforms.Perks_Panel));
        perkUI.Setup(item, count);
    }

    private void WeaponSetup(Item_SO weapon)
    {
        GetImage((int)Images.Weapon_Image).sprite = weapon.sprite;
    }

    private void GetWeapon(Item_SO item)
    {
        GetImage((int)Images.Weapon_Image).sprite = item.sprite;        
    }

    private void GetPerk(Item_SO item)
    {
        // 퍽을 얻었을 때 자식이 하나라도 있으면 그 UI가 어떤 UI 인지 확인
        Transform perkPanel = Get<Transform>((int)Transforms.Perks_Panel);
        if (perkPanel.childCount != 0)
        {
            // 모든 자식의 UI_Perk 확인하여 Sprite가 일치하면 다시 확인
            foreach (UI_Perk ui in perkPanel.GetComponentsInChildren<UI_Perk>())
            {
                if (ui.GetSprite() == item.sprite)
                {
                    if (item.isStackable) // 중첩 가능하면 Setup으로 중첩
                    {
                        ui.Setup(item);
                        UpdateTexts();
                        return;
                    }
                    else // 중첩 불가능하면 텍스트만 업데이트
                    {
                        UpdateTexts();
                        return;
                    }                         
                }
            }
        }
        UI_Perk perkUI = Managers.UI.MakeSub<UI_Perk>(Get<Transform>((int)Transforms.Perks_Panel));
        perkUI.Setup(item);
        UpdateTexts();
    }

    private void GetCodePiece()
    {
        GetText((int)Texts.CodePiece_Text).text = $"{Managers.GameManager.CodePiece}";
    }

    private void UpdateTexts()
    {
        GetText((int)Texts.CodePiece_Text); // To Do - 가지고 있는 코드조각 표기
        GetText((int)Texts.Atk_Text).text = $"{_playerStatManager.A_Atk}";
        GetText((int)Texts.FireRate_Text).text = $"{_playerStatManager.A_FireRate:F2}";
        GetText((int)Texts.MoveSpeed_Text).text = $"{_playerStatManager.MoveSpeed}";
        GetText((int)Texts.AddBullet_Text).text = $"{_playerStatManager.A_AddBullet}";
        GetText((int)Texts.Pierce_Text).text = $"{_playerStatManager.A_PierceCount}";
    }

    private void HPImageControl(int prevHp, int curHp)
    {
        // 실제 이미지의 개수
        int count = curHp / 2 + curHp % 2;

        // 이미지 개수와 Stack 개수가 같을 때
        if (_hpImages.Count == count)
        {
            Image image = _hpImages.Peek();

            if (curHp % 2 != 0) // curHp가 홀수 == 데미지를 받음
            {
                image.fillAmount = 0.5f;
            }
            else // curHp가 짝수 == 힐링
            {
                image.fillAmount = 1f;
            }
        }
        // 이미지 개수와 Stack 개수가 다를 때 == 이미지 개수에 변화 (삭제 or 추가)
        else
        {
            // curHp가 홀수 == 힐링
            if (curHp % 2 != 0)
            {
                Image image = Managers.UI.MakeSub<UI_Hp>(Get<Transform>((int)Transforms.HPImages_Panel)).GetComponent<Image>();
                image.fillAmount = 0.5f;
                _hpImages.Push(image);
            }
            // curHp가 짝수 == 데미지를 받음
            else
            {
                Image image = _hpImages.Pop();
                Managers.RM.Destroy(image.gameObject);
            }
        }
    }
}
