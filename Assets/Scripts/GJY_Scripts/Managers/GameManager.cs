using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Action OnGetCodePiece;

    public int day = 3;

    public int CodePiece { get; private set; } = 0;

    public void GetCodePiece()
    {
        CodePiece++;
        OnGetCodePiece?.Invoke();
    }

    public void Clear()
    {
        OnGetCodePiece = null;
    }
}
