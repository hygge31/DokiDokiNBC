using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseScene : MonoBehaviour
{
    public Define.TestScenes SceneType { get; protected set; }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        EventSystem eventSys = FindObjectOfType<EventSystem>();
        if (eventSys == null)
            Managers.RM.Instantiate("UI/@EventSystem");
    }

    public virtual void Clear() { }
}
