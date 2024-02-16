using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Popup : UI_Base
{
    private Graphic[] _graphics;

    protected override void Init()
    {
        Managers.UI.SetCanvas(gameObject);

        _graphics = GetComponentsInChildren<Graphic>();
        foreach (Graphic graphic in _graphics)
            StartCoroutine(FadeIn(graphic, 0, 1));
    }

    public void ClosePopup()
    {
        Managers.UI.ClosePopupUI(this);
    }

    private IEnumerator FadeIn(Graphic graphic, float start, float end)
    {
        if(graphic.name == "Blind")
            yield break;

        float current = 0;
        float percent = 0;
        float time = 0.1f;

        Color color = graphic.color;
        color.a = start;
        graphic.color = color;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / time;

            color.a = Mathf.Lerp(start, end, percent);
            graphic.color = color;

            yield return null;
        }
    }
}
