using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Action OnGetCodePiece;

    public int day = 5;

    public int CodePiece { get; private set; } = 0;
    public bool IsPCPowerOff { get; private set; } = false;

    public void GetCodePiece()
    {
        CodePiece++;
        OnGetCodePiece?.Invoke();
    }

    public void PCOff() => IsPCPowerOff = true;
    public void PCOn() => IsPCPowerOff = false;

    public void Clear()
    {
        OnGetCodePiece = null;
    }
}
