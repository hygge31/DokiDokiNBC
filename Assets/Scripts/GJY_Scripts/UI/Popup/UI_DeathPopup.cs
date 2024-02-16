using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DeathPopup : UI_Popup
{
    enum Images
    {
        Blind,
    }

    enum Buttons
    {
        Yes_btn,
    }

    enum Transforms
    {
        Panel,
    }

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        Bind<Transform>(typeof(Transforms));

        GetButton((int)Buttons.Yes_btn).onClick.AddListener(() => 
        {            
            Managers.Scene.LoadScene(Define.Scenes.Main);
            Managers.ResetGame();
        });

        StartCoroutine(BlindRoutine());
    }

    private IEnumerator BlindRoutine()
    {
        Get<Transform>((int)Transforms.Panel).gameObject.SetActive(false);

        float current = 0;
        float percent = 0;

        Image image = GetImage((int)Images.Blind);
        Color color = image.color;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 1;

            color.a = Mathf.Lerp(0,1, percent);
            image.color = color;

            yield return null;
        }

        Get<Transform>((int)Transforms.Panel).gameObject.SetActive(true);
    }
}
