using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hud : UI_Scene
{
    private Stack<Image> _hpImages = new Stack<Image>();

    enum Transforms
    {
        HPImages_Panel,
        Perks_Panel,
    }

    protected override void Init()
    {
        base.Init();

        //Bind<GameObject>(typeof(Transforms));
    }
}
