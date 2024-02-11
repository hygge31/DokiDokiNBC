using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (!recursive)
        {
            Transform transform = go.transform;
            for(int i = 0; i < transform.childCount; i++)
            {
                if(string.IsNullOrEmpty(name)||transform.GetChild(i).name == name)
                {
                    T component = transform.GetComponent<T>();
                    if(component != null) 
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }
}
