using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public enum DunGoneType
{
    Monster,
    Portal,
}


[System.Serializable]
public class RoomData 
{
    //Room SO

    public RoomDataSO roomData;

    public int roomNumber;
    public int width; //todo
    public int height; //todo

    public Vector2Int center;
    public BoundsInt bounds;


    [Header("Door")]
    //0305 refactoring
    public GameObject[] doorObjs = new GameObject[4];
    public Vector2Int[] doorPoints = new Vector2Int[4];
    public bool[] existDoors = new bool[4];
    public bool[] existedDoors = new bool[4];

    //0305 refactoring

    [Header("Room State")]
    public bool clear;
    public List<Door> doors = new List<Door>();
    public DunGoneType dungoenType;


    [Header("Camera")]
    public Camera _camera;
    public Vector2 minCamLimit;
    public Vector2 maxCamLimit;

    [Header("Spawn")]
    public List<Spawner> spawnerList = new List<Spawner>();
    public int clearCondition;
    public int curDeathMonster;




    public RoomData(RoomDataSO roomDataSO)
    {
        roomData = roomDataSO;
        width = roomData.width;
        height = roomData.height;
        dungoenType = DunGoneType.Monster;

    }


    public void SetRoomData(Vector2Int createPoint,int roomNumber)
    {
        center = createPoint;
        this.roomNumber = roomNumber;
        bounds = new BoundsInt(new Vector3Int(center.x - (width / 2), center.y - (height / 2), 0), new Vector3Int(width, height, 0));

        minCamLimit = new Vector2(center.x - width / 2, center.y - height / 2) + new Vector2(DunGoenManager.Instance.cameraWidth, DunGoenManager.Instance.cameraHeight);
        maxCamLimit = new Vector2(center.x + width / 2, center.y + height / 2) - new Vector2(DunGoenManager.Instance.cameraWidth, DunGoenManager.Instance.cameraHeight);

        //0305 refactoring  RTLB
        doorPoints[0] = new Vector2Int(center.x + (width / 2), center.y);
        doorPoints[1] = new Vector2Int(center.x, center.y + (height / 2));
        doorPoints[2] = new Vector2Int(center.x - (width / 2), center.y);
        doorPoints[3] = new Vector2Int(center.x, center.y - (height / 2));

        //0305 refactoring


    }




    public void AllDoorOff()
    {
        foreach (Door door in doors)
        {
            door.DoorOff();
        }
    }
 

    public void AppearAllDoor()
    {
        foreach (Door door in doors)
        {
            door.AppearanceDoor();
        }
    }

     public void ExitAllDoor()
     {
        foreach(Door door in doors)
        {
            door.ExitDoor();
        }
     }



    public void SpawnMonster()
    {
        if (!clear)
        {
            foreach(Spawner spawner in spawnerList)
            {
                spawner.Spawn();
            }
        }

    }


    public void DieMonsterAddAndClearCheck()
    {
        curDeathMonster++;

        if(clearCondition <= curDeathMonster) //Clear
        {
            if(dungoenType == DunGoneType.Portal)
            {
                DunGoenManager.Instance.CallOnActivePortal();
            }
            else
            {
                DunGoenManager.Instance.CreateItem(DunGoenManager.Instance.minimapSpriteList[roomNumber].transform);
            }

            clear = true;
            DunGoenManager.Instance.DungoenAllDoorAppear();
        }
    }


}
