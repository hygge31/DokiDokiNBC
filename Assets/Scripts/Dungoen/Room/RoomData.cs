using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class RoomData :MonoBehaviour
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

        leftDoorPoint = new Vector2Int(center.x - (width / 2), center.y);
        rightDoorPoint = new Vector2Int(center.x + (width / 2), center.y);
        topDoorPoint = new Vector2Int(center.x, center.y + (height / 2));
        bottomDoorPoint = new Vector2Int(center.x, center.y - (height / 2));
    }


    public void CreateDoor(RoomData nextRoom, int num) //
    {
        Transform container;

        if(!DunGoenManager.Instance.container.transform.Find($"Room {roomNumber}"))
        {
             container = new GameObject($"Room {roomNumber}").transform;
        }
        else
        {
            container = DunGoenManager.Instance.container.transform.Find($"Room {roomNumber}");
        }


        container.transform.SetParent(DunGoenManager.Instance.container.transform);
        switch (num)
        {
            case 0: //R
                //todo

                if (!rightDoor)
                {
                    if (!rightcDoor)
                    {
                        int ranY = Random.Range(center.y - height / 2 + 2, center.y + height / 2 - 2);
                        Vector2Int pot = new Vector2Int(center.x + (width / 2), ranY);
                        rightDoorPoint = pot;
                    }
                   

                    rightDoorObj = Instantiate(roomData.rightDoorObj, (Vector2)rightDoorPoint + Vector2.right*0.5f, Quaternion.identity);
                    GameObject tileDoor = Instantiate(roomData.tile_rightDoor, (Vector2)rightDoorPoint+Vector2.right*3, Quaternion.identity);

                    rightDoor = true;
                    nextRoom.leftcDoor = true;
                    nextRoom.leftDoorPoint = rightDoorPoint;
                    nextRoom.leftDoorPoint.x = nextRoom.center.x - nextRoom.width / 2;
                   
                    //todo
                    rightDoorObj.GetComponent<Door>().SetData(nextRoom, num, roomNumber,tileDoor, (Vector2)rightDoorPoint + Vector2.right * 3);
                    rightDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    doors.Add(rightDoorObj.GetComponent<Door>());

                    

                }
                break;
            case 1: //T
                if (!topDoor)
                {
                    if (!topcDoor)
                    {
                        int ranX = Random.Range(center.x - width / 2 + 2, center.x + width / 2 - 2);
                        Vector2Int pot = new Vector2Int(ranX, center.y + (height / 2));
                        topDoorPoint = pot;
                    }
                   

                    topDoorObj = Instantiate(roomData.topDoorObj, (Vector2)topDoorPoint, Quaternion.identity);
                    GameObject tileDoor = Instantiate(roomData.tile_topDoor, (Vector2)topDoorPoint, Quaternion.identity);


                    topDoor = true;
                    nextRoom.bottomcDoor = true;

                    nextRoom.bottomDoorPoint = topDoorPoint;
                    nextRoom.bottomDoorPoint.y = nextRoom.center.y - nextRoom.height / 2;

                    topDoorObj.GetComponent<Door>().SetData(nextRoom, num, roomNumber,tileDoor, (Vector2)topDoorPoint);
                    topDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    doors.Add(topDoorObj.GetComponent<Door>());

                    
                }
              
                break;
            case 2: //L
                if (!leftDoor)
                {
              
                    leftDoorObj = Instantiate(roomData.leftDoorObj, (Vector2)leftDoorPoint + Vector2.right*0.5f, Quaternion.identity);
                    GameObject tileDoor = Instantiate(roomData.tile_leftDoor, (Vector2)leftDoorPoint+Vector2.right*3, Quaternion.identity);

                    leftDoor = true;
                    nextRoom.rightcDoor = true;
                    leftDoorObj.GetComponent<Door>().SetData(nextRoom, num, roomNumber,tileDoor, (Vector2)leftDoorPoint + Vector2.right * 3);
                    leftDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    doors.Add(leftDoorObj.GetComponent<Door>());

                    
                }
                
                break;
            case 3: //B
                if (!bottomDoor)
                {
                    bottomDoorObj = Instantiate(roomData.bottomDoorObj, (Vector2)bottomDoorPoint, Quaternion.identity);
                    GameObject tileDoor = Instantiate(roomData.tile_bottomDoor, (Vector2)bottomDoorPoint+Vector2.up, Quaternion.identity);


                    bottomDoor = true;

                    nextRoom.topcDoor = true;
                    bottomDoorObj.GetComponent<Door>().SetData(nextRoom, num, roomNumber,tileDoor, (Vector2)bottomDoorPoint + Vector2.up);
                    bottomDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    doors.Add(bottomDoorObj.GetComponent<Door>());

                    
                }
                
                break;
        }


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
