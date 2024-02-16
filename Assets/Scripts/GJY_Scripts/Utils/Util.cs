using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null) 
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (!recursive)
        {
            Transform transform = go.transform;
            for (int i = 0; i < transform.childCount; i++)
            {
                if (string.IsNullOrEmpty(name) || transform.GetChild(i).name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static Transform FindParentFromHUD(string name = null)
    {
        GameObject hud = GameObject.Find("UI_Hud");
        if(hud == null)
        {
            Debug.Log("현재 씬에 Hud가 존재하지 않습니다.");
            return null;
        }

        Transform parent = FindChild<Transform>(hud, name, true);
        if (parent == null)
        {
            Debug.Log($"해당 Transform이 존재하지 않습니다. : {name}");
            return null;
        }

        return parent;
    }
}
