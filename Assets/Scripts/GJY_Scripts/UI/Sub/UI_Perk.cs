using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Perk : UI_Base
{
    private UI_EventHandler _handler;
    private Text _infoText;
    private Text _nameText;
    private Image _image;
    private Item_SO _item;

    private int _count;

    enum Texts
    {
        Count_Text,
    }

    protected override void Init()
    {
        base.Init();

        _handler = GetComponent<UI_EventHandler>();
        _image = GetComponent<Image>();
        _infoText = GetComponentInParent<Text>();

        _handler.OnEnterHandler += ShowItemInfo;
        _handler.OnExitHandler += HideItemInfo;

        BindText(typeof(Texts));
        BindInfoTextInParent();
    }

    private void BindInfoTextInParent()
    {
        // To Do - HUD 안의 Text를 가져오기.
        _infoText = Util.FindParentFromHUD("PerkInfo_Text").GetComponent<Text>();
        _nameText = Util.FindParentFromHUD("PerkName_Text").GetComponent<Text>();
    }

    public void Setup(Item_SO item)
    {
        _item = item;
        _image.sprite = item.sprite;
        _count++;        

        GetText((int)Texts.Count_Text).text = $"X {_count}";
    }

    private void ShowItemInfo(PointerEventData data)
    {
        _nameText.text = $"{_item.displayName}";
        _infoText.text = $"{_item.displayDesc}";
    }

    private void HideItemInfo(PointerEventData data)
    {
        _nameText.text = "";
        _infoText.text = "";
    }
}
