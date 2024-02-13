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
    





    [Header("Game State")]
    public int curDungoenRoomNumber;

    [Header("Room Move Panel")]
    public GameObject panel;


    [Header("Minimap part")]
    public GameObject minimapUi;
    public GameObject minimapCamera;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        Instantiate(minimapCamera);
        Instantiate(minimapUi);


        CreateDunGoen();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            DungoenAllDoorExit();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DungoenAllDoorAppear();
        }
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
   
}
