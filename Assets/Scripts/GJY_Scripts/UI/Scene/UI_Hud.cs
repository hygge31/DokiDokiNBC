using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Hud : UI_Scene
{
    private Stack<Image> _hpImages = new Stack<Image>();

    enum Transforms
    {
        HPImages_Panel,
        Perks_Panel,
    }

    enum Images
    {
        Weapon_Image,
    }

    protected override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(Transforms));
        BindImage(typeof(Images));

        Managers.Player.OnPlayerSetup += PlayerSetup;
        Managers.Player.OnApplyPerkStat += GetPerk;
        Managers.Player.OnHealing += HPImageControl;
        Managers.Player.OnGetDamaged += HPImageControl;

        Managers.Attack.OnWeaponSetup += WeaponSetup;
        Managers.Attack.OnChangeWeapon += GetWeapon;
    }

    private void PlayerSetup()
    {
        int hp = Managers.Player.Hp;
        int count = hp / 2 + hp % 2;
        
        for(int i = 0; i < count; i++)
        {
            Image image = Managers.UI.MakeSub<UI_Hp>(Get<Transform>((int)Transforms.HPImages_Panel)).GetComponent<Image>();
            _hpImages.Push(image);
        }

        if (hp % 2 != 0)
        {
            _hpImages.Peek().fillAmount = 0.5f;
        }        
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
        UI_Perk perkUI = Managers.UI.MakeSub<UI_Perk>(Get<Transform>((int)Transforms.Perks_Panel));
        perkUI.Setup(item);
    }

    private void HPImageControl(int prevHp, int curHp)
    {
        // 실제 이미지의 개수
        int count = curHp / 2 + curHp % 2;

        // 이미지 개수와 Stack 개수가 같을 때
        if(_hpImages.Count == count)
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
