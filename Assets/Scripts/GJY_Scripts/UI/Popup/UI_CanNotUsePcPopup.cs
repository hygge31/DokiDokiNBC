using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CanNotUsePcPopup : UI_Popup
{
    enum Texts
    {
        GoToSleep_Text,
    }

    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));

        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float current = 0;
        float percent = 0;

        Text text = GetText((int)Texts.GoToSleep_Text);
        Color color = text.color;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 2;

            text.fontSize++;
            color.a = Mathf.Lerp(1, 0, percent);
            text.color = color;

            yield return null;
        }

        ClosePopup();
    }
}
