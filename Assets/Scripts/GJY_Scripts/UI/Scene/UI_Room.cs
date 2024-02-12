using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Room : UI_Scene
{
    enum Pointer
    {
        Bed_Pointer,
        PC_Pointer,
    }

    protected override void Init()
    {
        base.Init();

        Bind<UI_EventHandler>(typeof(Pointer));        

        Get<UI_EventHandler>((int)Pointer.Bed_Pointer).OnEnterHandler += BedEnter;        
        Get<UI_EventHandler>((int)Pointer.Bed_Pointer).OnExitHandler += BedExit;
        Get<UI_EventHandler>((int)Pointer.Bed_Pointer).OnClickHandler += BedClick;

        Get<UI_EventHandler>((int)Pointer.PC_Pointer).OnEnterHandler += PCEnter;
        Get<UI_EventHandler>((int)Pointer.PC_Pointer).OnExitHandler += PCExit;        
        Get<UI_EventHandler>((int)Pointer.PC_Pointer).OnClickHandler += PCClick;
    }

    private void BedEnter(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1.1f, true));
    private void BedExit(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1, false));
    private void BedClick(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.Bed_Pointer, 1, false));

    private void PCEnter(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1.1f, true));
    private void PCExit(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1, false));
    private void PCClick(PointerEventData data) => StartCoroutine(HighLight((int)Pointer.PC_Pointer, 1, false));


    private IEnumerator HighLight(int typeIndex, float endSize, bool active)
    {
        GameObject go = Get<UI_EventHandler>(typeIndex).gameObject;
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
