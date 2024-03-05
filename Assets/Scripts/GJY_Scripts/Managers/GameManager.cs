using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Action OnGetCodePiece;

    public int day = 2;

    public int CodePiece { get; private set; } = 0;
    public bool IsPCPowerOff { get; private set; } = false;

    public void GetCodePiece()
    {
        CodePiece++;
        OnGetCodePiece?.Invoke();
    }

    public void PCOff() => IsPCPowerOff = true;
    public void PCOn() => IsPCPowerOff = false;

    public void Upgrade(int cost)
    {
        CodePiece -= cost;
    }

    public void Clear()
    {
        OnGetCodePiece = null;
    }

    public void ResetGame()
    {
        day = 1;
        CodePiece = 0;
    }
}
