using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSprite : MonoBehaviour
{

    public DunGoneType dunGoneType;


    public SpriteRenderer defaultSprite;
    public SpriteRenderer curPositionSprite;
    public SpriteRenderer questionMark;
    public SpriteRenderer portal;



    public void CurPosition()
    {
        if(questionMark.enabled == true)
        {
            questionMark.enabled = false;
        }

        if(dunGoneType == DunGoneType.Portal)
        {
            portal.enabled = true;
        }

        curPositionSprite.enabled = true;
        
    }

    public void OutPoisition()
    {
        curPositionSprite.enabled = false;
        defaultSprite.color = Color.white;
    }


}
