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

    [Header("Room State")]
    public bool clear;
    public List<GameObject> doors = new List<GameObject>();



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
                rightDoorObj = Instantiate(roomData.rightDoorObj, (Vector2)rightDoorPoint, Quaternion.identity);
                rightDoorObj.GetComponent<Door>().SetData(nextRoom, num,roomNumber);
                rightDoorObj.transform.SetParent(container.transform);
                doors.Add(rightDoorObj);
                break;
            case 1: //T
                topDoorObj = Instantiate(roomData.topDoorObj, (Vector2)topDoorPoint , Quaternion.identity);
                topDoorObj.GetComponent<Door>().SetData(nextRoom, num,roomNumber);
                topDoorObj.transform.SetParent(container.transform);
                doors.Add(topDoorObj);
                break;
            case 2: //L
                leftDoorObj = Instantiate(roomData.leftDoorObj, (Vector2)leftDoorPoint, Quaternion.identity);
                leftDoorObj.GetComponent<Door>().SetData(nextRoom, num,roomNumber);
                leftDoorObj.transform.SetParent(container.transform);
                doors.Add(leftDoorObj) ;
                break;
            case 3: //B
                bottomDoorObj = Instantiate(roomData.bottomDoorObj, (Vector2)bottomDoorPoint, Quaternion.identity);
                bottomDoorObj.GetComponent<Door>().SetData(nextRoom, num,roomNumber);
                bottomDoorObj.transform.SetParent(container.transform);
                doors.Add(bottomDoorObj);
                break;
        }
    }

    public void ToggleDoor()
    {
        foreach(GameObject door in doors)
        {
            if (door.activeSelf)
            {
                //퇴장 애니메이션
                door.SetActive(false);
            }
            else
            {
                //등장 애니메이션
                door.SetActive(true);
                //door.GetComponent<Door>().AppearanceDoor();
            }

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
