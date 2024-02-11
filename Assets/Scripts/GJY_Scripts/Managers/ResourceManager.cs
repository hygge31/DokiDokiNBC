using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //public T Load<T>(string path) where T : UnityEngine.Object
    //{
    //    if (typeof(T) == typeof(GameObject))
    //    {
    //        string name = path;
    //        int index = name.LastIndexOf('/');
    //        if (index >= 0)
    //            name = name.Substring(index + 1);

    //        GameObject go = Managers.Pool.GetOriginal(name);
    //        if (go != null)
    //            return go as T;
    //    }

    //    return Resources.Load<T>(path);
    //}

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject origin = Resources.Load<GameObject>($"Prefabs/{path}");

        if (origin == null)
        {
            Debug.Log($"오브젝트 불러오기에 실패했습니다. : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(origin, parent);
        go.name = origin.name;

        return go;
    }
}
