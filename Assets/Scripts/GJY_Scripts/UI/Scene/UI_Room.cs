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

    protected override void Init()
    {
        base.Init();

        BindUIEventHandler(typeof(Pointer));   
        BindAnimator(typeof(Animator));

        GetUIEventHandler((int)Pointer.Bed_Pointer).OnEnterHandler += BedEnter;        
        GetUIEventHandler((int)Pointer.Bed_Pointer).OnExitHandler += BedExit;
        GetUIEventHandler((int)Pointer.Bed_Pointer).OnClickHandler += BedClick;

        GetUIEventHandler((int)Pointer.PC_Pointer).OnEnterHandler += PCEnter;
        GetUIEventHandler((int)Pointer.PC_Pointer).OnExitHandler += PCExit;
        GetUIEventHandler((int)Pointer.PC_Pointer).OnClickHandler += PCClick;
    }

    private void BedEnter(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1.1f, true));
    private void BedExit(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1, false));
    private void BedClick(PointerEventData data)
    {
        SceneManager.LoadScene("GJY2");
    }

    private void PCEnter(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1.1f, true));
    private void PCExit(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1, false));
    private void PCClick(PointerEventData data)
    {
        GetAnimator((int)Animator.Clock_Image).SetTrigger("UsePC");
        StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1, false));
        Managers.UI.ShowPopupUI<UI_Shop>();
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
