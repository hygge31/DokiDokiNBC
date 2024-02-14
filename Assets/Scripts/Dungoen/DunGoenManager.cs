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

    [Header("Room Move Panel")]
    public GameObject panel;


    [Header("Minimap part")]
    public GameObject minimapUi;
    public GameObject minimapCamera;

    public event Action OnChangeMinimap;
    public event Action<RoomData> OnMoveToDungoenRoom;


    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        Instantiate(minimapCamera);
        Instantiate(minimapUi);

        
        CreateDunGoen();
        DungoenAllDoorAppear();
    }


    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space)){
            DungoenAllDoorExit();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DungoenAllDoorAppear();
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
        playerTransform = Instantiate(playerTransform.gameObject,Vector3.zero, Quaternion.identity).transform;
        CallOnMoveToDungoenRoom(dungoenRoomDataList[0]);
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
        curDungoenRoomNumber = nextRoomNumber;
        dungoenRoomDataList[curDungoenRoomNumber].SpawnMonster();
        minimapSpriteList[curRoomNumber].GetComponent<MinimapSprite>().OutPoisition();
        minimapSpriteList[nextRoomNumber].GetComponent<MinimapSprite>().CurPosition();
        CallOnChangeMinimap();
        CallOnMoveToDungoenRoom(dungoenRoomDataList[nextRoomNumber]);
    }


    

}
