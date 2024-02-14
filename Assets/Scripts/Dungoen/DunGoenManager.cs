using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class DunGoenManager : MonoBehaviour
{
    public static DunGoenManager Instance;
    public GameObject container;

    [Header("Dungoen Generator")]
    public DungoenGenerator dungoenGenerator;
    public List<RoomData> dungoenRoomDataList = new List<RoomData>(); 
    public List<GameObject> minimapSpriteList = new List<GameObject>();

    [Header("Player")]
    public Transform playerTransform;

    [Header("Game State")]
    public int curDungoenRoomNumber;
    public int count; // init
    [Header("Room Move Panel")]
    public GameObject panel;

    [Header("Minimap part")]
    public GameObject minimapUi;
    public GameObject minimapCamera;

    [Header("Camera")]
    public Camera _camera;
    public float cameraWidth;
    public float cameraHeight;

    
    public event Action OnChangeMinimap;
    public event Action<RoomData> OnMoveToDungoenRoom;
    public event Action OnActivePortal;

    private void Awake()
    {
        Instance = this;

        _camera = Camera.main;
        Instantiate(minimapCamera);
        Instantiate(minimapUi);
        Instantiate(playerTransform);


        cameraWidth = _camera.orthographicSize * _camera.aspect - 1;
        cameraHeight = _camera.orthographicSize -2;

    }

    private void Start()
    {
        CreateDunGoen();
        DungoenAllDoorAppear();
        CallOnMoveToDungoenRoom(dungoenRoomDataList[0]);
        
    }


    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space)){
            DungoenAllDoorAppear();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            CallOnActivePortal();
        }
        //test
    }


    public void CallOnChangeMinimap()
    {
        OnChangeMinimap?.Invoke();
    }
    public void CallOnMoveToDungoenRoom(RoomData roomData)
    {
        OnMoveToDungoenRoom?.Invoke(roomData);
    }
    public void CallOnActivePortal()
    {
        OnActivePortal?.Invoke();
    }


    public void CreateDunGoen()
    {
        if(container != null)
        {
            DestroyImmediate(container.gameObject);
        }

        GameObject newContainer = new GameObject("Container");
        newContainer.transform.SetParent(gameObject.transform);
        container = newContainer;

        ClearList();

        dungoenGenerator.ProcedurealDungoenGenerator();
        minimapSpriteList[0].GetComponent<MinimapSprite>().CurPosition();
        
    }


    public void DungoenAllDoorExit()
    {
        foreach(RoomData room in dungoenRoomDataList)
        {
            room.ExitAllDoor();
        }
    }

    public void DungoenAllDoorAppear()
    {
        foreach (RoomData room in dungoenRoomDataList)
        {
            room.AppearAllDoor();
        }
    }


    public void ClearList()
    {
        dungoenRoomDataList.Clear();
        minimapSpriteList.Clear();
    }
   

    public void MoveToDungoen(int curRoomNumber ,int nextRoomNumber)
    {
        if (!dungoenRoomDataList[nextRoomNumber].clear)
        {
            count++;
        }


        curDungoenRoomNumber = nextRoomNumber;
        dungoenRoomDataList[nextRoomNumber].SpawnMonster();
        minimapSpriteList[curRoomNumber].GetComponent<MinimapSprite>().OutPoisition();
        minimapSpriteList[nextRoomNumber].GetComponent<MinimapSprite>().CurPosition();
        CallOnChangeMinimap();
        CallOnMoveToDungoenRoom(dungoenRoomDataList[nextRoomNumber]);

        if (!dungoenRoomDataList[nextRoomNumber].clear)
        {
            DungoenAllDoorExit();
        }

    }


    public void DieMonster()
    {
        dungoenRoomDataList[curDungoenRoomNumber].DieMonsterAddAndClearCheck();
    }





}
