using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSprite : MonoBehaviour
{
    public SpriteRenderer defaultSprite;
    public SpriteRenderer curPositionSprite;
    public SpriteRenderer questionMark;
    




    public void CurPosition()
    {
        if(questionMark.enabled == true)
        {
            questionMark.enabled = false;
        }
        curPositionSprite.enabled = true;
        
    }

    public void OutPoisition()
    {
        curPositionSprite.enabled = false;
        defaultSprite.color = Color.white;
    }


}
