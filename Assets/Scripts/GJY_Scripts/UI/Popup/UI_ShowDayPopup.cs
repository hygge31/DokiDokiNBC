using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShowDayPopup : UI_Popup
{
    enum Texts
    {
        Day_Text,
    }

    enum Images
    {
        Blind,
    }

    protected override void Init()
    {
        base.Init();

        BindText(typeof(Texts));
        BindImage(typeof(Images));

        Text text = GetText((int)Texts.Day_Text);
        text.text = $"Day {Managers.GameManager.day}...";        

        StartCoroutine(FadeBlindRoutine());
    }

    private IEnumerator FadeBlindRoutine()
    {
        Text text = GetText((int)Texts.Day_Text);
        Color colorText = text.color;
        colorText.a = 0;

        Image image = GetImage((int)Images.Blind);
        Color colorImage = image.color;

        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 1.5f;

            colorImage.a = Mathf.Lerp(1, 0, percent);
            colorText.a = Mathf.Lerp(0, 1, percent);
            image.color = colorImage;
            text.color = colorText;

            yield return null;
        }

        current = 0;
        percent = 0;
        yield return new WaitForSeconds(1);

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 1;

            colorText.a = Mathf.Lerp(1, 0, percent);
            text.color = colorText;

            yield return null;
        }

        ClosePopup();        
    }
}
