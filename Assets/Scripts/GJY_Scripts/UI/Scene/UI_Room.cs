using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Room : UI_Scene
{
    enum Pointer
    {
        Bed_Pointer,
        PC_Pointer,
    }

    enum Animator
    {
        Clock_Image,
    }

    enum Images
    {
        Blind,
        BG,
    }

    protected override void Init()
    {
        base.Init();

        BindUIEventHandler(typeof(Pointer));
        BindAnimator(typeof(Animator));
        BindImage(typeof(Images));

        GetUIEventHandler((int)Pointer.Bed_Pointer).OnEnterHandler += BedEnter;
        GetUIEventHandler((int)Pointer.Bed_Pointer).OnExitHandler += BedExit;
        GetUIEventHandler((int)Pointer.Bed_Pointer).OnClickHandler += BedClick;

        if(Managers.GameManager.day == 5)
        {
            GetUIEventHandler((int)Pointer.Bed_Pointer).gameObject.SetActive(false);
            GetImage((int)Images.BG).sprite = Resources.Load<Sprite>("Sprites/UI/OnlyHan");
            GetAnimator((int)Animator.Clock_Image).Play("DigitalClock_404");
        }            

        GetUIEventHandler((int)Pointer.PC_Pointer).OnEnterHandler += PCEnter;
        GetUIEventHandler((int)Pointer.PC_Pointer).OnExitHandler += PCExit;
        GetUIEventHandler((int)Pointer.PC_Pointer).OnClickHandler += PCClick;

        StartCoroutine(BlindFadeOut());
    }

    private IEnumerator BlindFadeOut()
    {
        float current = 0;
        float percent = 0;
        float time = 1.5f;

        Image blind = GetImage((int)Images.Blind);
        Color color = blind.color;        

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            color.a = Mathf.Lerp(1, 0, percent);
            blind.color = color;

            yield return null;
        }
    }

    private void BedEnter(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1.1f, true));
    private void BedExit(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1, false));
    private void BedClick(PointerEventData data)
    {
        if (Managers.GameManager.IsPCPowerOff)
            Managers.Scene.LoadScene(Define.Scenes.Dungeon);
    }

    private void PCEnter(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1.1f, true));
    private void PCExit(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1, false));
    private void PCClick(PointerEventData data)
    {
        GetAnimator((int)Animator.Clock_Image).SetTrigger("UsePC");
        StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1, false));
        if (!Managers.GameManager.IsPCPowerOff)
        {
            if (Managers.GameManager.day == 5)
                Managers.UI.ShowPopupUI<UI_EndingShop>();
            else
                Managers.UI.ShowPopupUI<UI_Shop>();
        }
        else
            Managers.UI.ShowPopupUI<UI_CanNotUsePcPopup>();
    }

    private IEnumerator HighLight(int typeIndex, float endSize, bool active)
    {
        GameObject go = GetUIEventHandler(typeIndex).gameObject;
        go.GetComponent<Outline>().enabled = active;

        float current = 0;
        float percent = 0;
        float time = 0.1f;

        Vector3 startScale = go.transform.localScale;
        Vector3 endScale = Vector3.one * endSize;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            go.transform.localScale = Vector3.Lerp(startScale, endScale, percent);

            yield return null;
        }
    }
}
