using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InputActionHandler : MonoBehaviour
{
    public Action OnEnterInvoke;
    public Action OnEscInvoke;    

    private void Update()
    {
        HandleKeyInput(KeyCode.Return, OnEnterInvoke);
        HandleKeyInput(KeyCode.Escape, OnEscInvoke);
    }

    private void HandleKeyInput(KeyCode key, Action action)
    {
        if (Input.GetKeyDown(key))
            action?.Invoke();
    }
}
