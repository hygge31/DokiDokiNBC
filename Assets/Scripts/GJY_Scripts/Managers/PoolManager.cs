using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Origin {  get; private set; }
        public Transform Root { get; set; }

        Queue<Poolable> _poolQueue = new Queue<Poolable>();

        public void Init(GameObject origin, int count)
        {
            Origin = origin;
            Root = new GameObject($"{origin.name}_Root").transform;

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        private Poolable Create()
        {
            GameObject go = Object.Instantiate(Origin);
            go.transform.SetParent(Root);
            go.name = Origin.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;
            poolable.transform.position = new Vector3(100, 0, 0);

            _poolQueue.Enqueue(poolable);
        }

        public Poolable Pop()
        {
            Poolable poolable;

            if (_poolQueue.Count > 0)
                poolable = _poolQueue.Dequeue();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);            
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    private Dictionary<string, Pool> _poolDict = new Dictionary<string, Pool>();
    private Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject("@Pool_Root").transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject origin, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(origin, count);
        pool.Root.parent = _root;

        _poolDict.Add(origin.name, pool);
    }

    public void Push(Poolable poolable)
    {        
        if (_poolDict.ContainsKey(poolable.name) == false)
        {
            Object.Destroy(poolable.gameObject);
            return;
        }

        _poolDict[poolable.name].Push(poolable);
    }

    public Poolable Pop(GameObject origin, Transform parent = null)
    {
        if(_poolDict.ContainsKey(origin.name) == false)
            CreatePool(origin);

        return _poolDict[origin.name].Pop();
    }

    public GameObject GetOrigin(string name)
    {
        if (_poolDict.ContainsKey(name) == false)
            return null;

        return _poolDict[name].Origin;
    }

    public void Clear()
    {
        foreach (Transform child in _root)
            Object.Destroy(child.gameObject);

        _poolDict.Clear();
    }
}
