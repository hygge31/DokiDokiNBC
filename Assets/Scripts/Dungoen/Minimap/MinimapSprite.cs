using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSprite : MonoBehaviour
{
    public SpriteRenderer defaultSprite;
    public SpriteRenderer curPositionSprite;
    public SpriteRenderer questionMark;
    Color color = new Color(133, 133, 133);






    public void CurPosition()
    {
        if(questionMark.enabled == true)
        {
            questionMark.enabled = false;
        }
        curPositionSprite.enabled = true;
        defaultSprite.color = color;
    }

    public void OutPoisition()
    {
        curPositionSprite.enabled = false;
        defaultSprite.color = Color.white;
    }


}
