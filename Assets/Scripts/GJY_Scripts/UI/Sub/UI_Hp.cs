using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Hp : UI_Base
{
    private Image image;

    protected override void Init()
    {
        base.Init();

        image = GetComponent<Image>();
    }
}
