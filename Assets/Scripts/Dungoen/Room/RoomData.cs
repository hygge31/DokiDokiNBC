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
    public Vector2Int leftDoorPoint;
    public Vector2Int rightDoorPoint;
    public Vector2Int topDoorPoint;
    public Vector2Int bottomDoorPoint;

    public GameObject leftDoorObj;
    public GameObject rightDoorObj;
    public GameObject topDoorObj;
    public GameObject bottomDoorObj;


    public bool leftDoor;
    public bool rightDoor;
    public bool topDoor;
    public bool bottomDoor;

    public bool leftcDoor;
    public bool rightcDoor;
    public bool topcDoor;
    public bool bottomcDoor;

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


        leftDoorPoint = new Vector2Int(center.x - (width / 2), center.y);
        rightDoorPoint = new Vector2Int(center.x + (width / 2), center.y);
        topDoorPoint = new Vector2Int(center.x, center.y + (height / 2));
        bottomDoorPoint = new Vector2Int(center.x, center.y - (height / 2));
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

            clear = true;
            DunGoenManager.Instance.DungoenAllDoorAppear();
        }
    }


}
