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
    public List<RoomData> dungoenRoomDataList = new List<RoomData>(); //

    public List<GameObject> minimapSpriteList = new List<GameObject>();
    


    [Header("Game State")]
    public int curDungoenRoomNumber;

    [Header("Room Move Panel")]
    public GameObject panel;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        CreateDunGoen();
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
    }


    public void MoveToDungoenRoom(int dungoenRoomNumber)
    {
        curDungoenRoomNumber = dungoenRoomNumber;
        dungoenRoomDataList[curDungoenRoomNumber].SpawnMonster();

        //MiniMap

    }


    public void ClearList()
    {
        dungoenRoomDataList.Clear();
        minimapSpriteList.Clear();
    }
   
}
