using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSprite : MonoBehaviour
{

    public DunGoneType dunGoneType;


    public SpriteRenderer defaultSprite;
    public GameObject curPositionSprite;
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

        curPositionSprite.SetActive(true);
        
    }

    public void OutPoisition()
    {
        curPositionSprite.SetActive(false);
        defaultSprite.color = Color.white;
    }


}
