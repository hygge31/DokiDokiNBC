using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : UI_Scene
{
    [SerializeField] AnimationCurve curve;

    enum Buttons
    {
        Start_Btn,
    }

    enum Images
    {
        Title_Image,
    }

    protected override void Init()
    {
        base.Init();

        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        GetButton((int)Buttons.Start_Btn).onClick.AddListener(() => Managers.Scene.LoadScene(Define.Scenes.Main));

        StartCoroutine(BounceRoutine(Vector3.one, Vector3.one * 0.9f));
    }

    private IEnumerator BounceRoutine(Vector3 startSize, Vector3 endSize)
    {
        float current = 0;
        float percent = 0;

        Image titleImage = GetImage((int)Images.Title_Image);        

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / 1;

            titleImage.transform.localScale = Vector3.Lerp(startSize, endSize, curve.Evaluate(percent));

            yield return null;
        }

        StartCoroutine(BounceRoutine(endSize, startSize));
    }
}
