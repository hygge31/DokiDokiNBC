using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Ending : UI_Scene
{
    enum Images
    {
        Blind,
    }

    protected override void Init()
    {
        base.Init();

        BindImage(typeof(Images));

        StartCoroutine(HanAnounce());
    }

    private IEnumerator HanAnounce()
    {
        yield return new WaitForSeconds(6);
        SoundManager.Instance.PlayClip(Resources.Load<AudioClip>("Sounds/UI/OnlyHanCut"));
        yield return new WaitForSeconds(1.75f);
        GetImage((int)Images.Blind).color = Color.black;
        SoundManager.Instance.PlayClip(Resources.Load<AudioClip>("Sounds/UI/Doom"));
    }
}
