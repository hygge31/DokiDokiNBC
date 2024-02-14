using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

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

    [Header("Camera")]
    public Camera _camera;
    public Vector2 minCamLimit;
    public Vector2 maxCamLimit;




    public RoomData(RoomDataSO roomDataSO)
    {
        roomData = roomDataSO;
        width = roomData.width;
        height = roomData.height;

    }


    public void SetRoomData(Vector2Int createPoint,int roomNumber)
    {
        center = createPoint;
        this.roomNumber = roomNumber;
        bounds = new BoundsInt(new Vector3Int(center.x - (width / 2), center.y - (height / 2), 0), new Vector3Int(width, height, 0));

        minCamLimit = new Vector2(center.x - width / 2, center.y - height / 2);
        maxCamLimit = new Vector2(center.x + width / 2, center.y + height / 2);


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
            //DungoenManager All door Set active false
            Debug.Log("Stage Change Message and spawn monster");
        }

    }


}
