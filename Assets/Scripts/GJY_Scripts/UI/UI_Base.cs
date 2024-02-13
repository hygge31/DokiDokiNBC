using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    private Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private void Awake()
    {
        Init();
    }

    protected virtual void Init() { }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (type == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"바인드에 실패했습니다. : {type.Name}");
        }
    }

    protected void BindButton(Type type) => Bind<Button>(type);
    protected void BindImage(Type type) => Bind<Image>(type);
    protected void BindText(Type type) => Bind<Text>(type);
    protected void BindInputField(Type type) => Bind<InputField>(type);
    protected void BindUIEventHandler(Type type) => Bind<UI_EventHandler>(type);
    protected void BindAnimator(Type type) => Bind<Animator>(type);

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            Debug.Log($"해당 키가 없습니다. : {typeof(T).Name}");
            return null;
        }

        return objects[index] as T;
    }

    protected Button GetButton(int index) => Get<Button>(index);
    protected Image GetImage(int index) => Get<Image>(index);
    protected Text GetText(int index) => Get<Text>(index);
    protected InputField GetInputField(int index) => Get<InputField>(index);
    protected UI_EventHandler GetUIEventHandler(int index) => Get<UI_EventHandler>(index);
    protected Animator GetAnimator(int index) => Get<Animator>(index);
}
