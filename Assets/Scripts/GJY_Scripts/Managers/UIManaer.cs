using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManaer
{
    private Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    private UI_Scene _scene = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject("@UI_Root");
                return root;
            }
            return root;
        }
    }

    private int _order = 5;

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = go.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.RM.Instantiate($"UI/Scene/{name}");
        T scene = go.GetOrAddComponent<T>();
        _scene = scene;

        scene.transform.SetParent(Root.transform);

        return scene;
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.RM.Instantiate($"UI/Scene/{name}");
        T popup = go.GetOrAddComponent<T>();
        _popupStack.Push(popup);

        popup.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
            return;

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();

        Object.Destroy(popup.gameObject);
        _order--;
    }

    public void CloseAllPopupUI()
    {
        foreach (UI_Popup popup in _popupStack)
            popup.ClosePopup();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _scene = null;
    }
}
