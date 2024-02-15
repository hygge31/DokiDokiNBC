using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region Contents
    AttackManager _attack = new AttackManager();
    DropManager _drop = new DropManager();
    PlayerStatManager _playerStat = new PlayerStatManager();
    GameManager _gameManager = new GameManager();


    public static AttackManager Attack => Instance?._attack;
    public static DropManager Drop => Instance?._drop;
    public static PlayerStatManager Player => Instance?._playerStat;
    public static GameManager GameManager => Instance?._gameManager;
    #endregion

    #region Core
    PoolManager _poolManager = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _sceneManager = new SceneManagerEx();
    UIManaer _uiManager = new UIManaer();


    public static PoolManager Pool => Instance?._poolManager;
    public static ResourceManager RM => Instance?._resource;
    public static SceneManagerEx Scene => Instance?._sceneManager;
    public static UIManaer UI => Instance?._uiManager;

    #endregion

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");

            if(go == null)
            {
                go = new GameObject("@Managers");
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
                        
            Player.Init();
            Attack.Init();
        }
    }

    public static void Clear()
    {
        UI.Clear();
        Player.Clear();
        Attack.Clear();
        GameManager.Clear();
        Pool.Clear();
    }
}
