using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

public class DungoenGenerator : MonoBehaviour
{
    [Header("Draw Tilemap")]
    public TileDraw tileDrawer;

    [Header("Minimap")]
    public GameObject minimapSpriteObj;
    public GameObject rowLine;
    public GameObject colLine;
    public HashSet<LinePath> minimapLineList = new HashSet<LinePath>();

    [Header("Dungoen Generator")]
    public int maxRoomCount = 12;
    public int offset = 10;
    public LayerMask dungoenlayer;

    public List<RoomDataSO> roomDataSOs = new List<RoomDataSO>();
    public List<Vector2Int> path = new List<Vector2Int>();

    List<Vector2Int> dir = new List<Vector2Int>()
    {
        new Vector2Int(1,0),    //R
        new Vector2Int(0,1),    //T
        new Vector2Int(-1,0),   //L
        new Vector2Int(0,-1),   //B
    };


    public void ProcedurealDungoenGenerator()
    {
        path = RandomCreateRoomPosition(Vector2Int.zero, maxRoomCount); //todo fix player position,
        tileDrawer.DrawAllTile();
        CreateDoor();

        DungoenuAllDoorOff();


        //Create minimap
        CreateMinimap();
        
    }


    public List<Vector2Int> RandomCreateRoomPosition(Vector2Int startPoint,int maxRoomCount)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        Vector2Int curPoint = startPoint;
        // -- Init
        path.Add(curPoint);
        RoomData curRoomData = new RoomData(roomDataSOs[Random.Range(0,roomDataSOs.Count)]);
        curRoomData.SetRoomData(curPoint,0);
        DunGoenManager.Instance.dungoenRoomDataList.Add(curRoomData);
        // -- Init


        for (int i = 1; i < maxRoomCount; i++)
        {
            curRoomData = new RoomData(roomDataSOs[Random.Range(0, roomDataSOs.Count)]);
            curPoint += GetCreatePoint(curRoomData);

            while (path.Contains(curPoint))
            {
                curPoint += GetCreatePoint(curRoomData);
            }
            path.Add(curPoint);
            curRoomData.SetRoomData(curPoint,i);
            
            DunGoenManager.Instance.dungoenRoomDataList.Add(curRoomData);
        }
        return path;
    }


    void CreateDoor()
    {
        foreach(RoomData roomData in DunGoenManager.Instance.dungoenRoomDataList)
        {
            for (int i = 0; i < dir.Count; i++)
            {
                Vector2Int checkNextRoomPoint = dir[i];
                checkNextRoomPoint *= new Vector2Int(roomData.width+ 10, roomData.height + 10);
                checkNextRoomPoint = roomData.center + checkNextRoomPoint;
                if (path.Contains(checkNextRoomPoint))
                {
                    RoomData nextRoomData = FindRoomdata(checkNextRoomPoint);
                    CreateDoor(roomData,nextRoomData, i);
                }
            }
        }
    }
    #region Door-------------------------------------------------------------------------------------------------
    public void CreateDoor(RoomData curRommData ,RoomData nextRoom, int num) //
    {
        Transform container;

        if (!DunGoenManager.Instance.container.transform.Find($"Room {curRommData.roomNumber}"))
        {
            container = new GameObject($"Room {curRommData.roomNumber}").transform;
        }
        else
        {
            container = DunGoenManager.Instance.container.transform.Find($"Room {curRommData.roomNumber}");
        }


        container.transform.SetParent(DunGoenManager.Instance.container.transform);

        int width = curRommData.width;
        int height = curRommData.height;



        switch (num)
        {
            case 0: //R
                //todo

                if (!curRommData.rightDoor)
                {
                    if (!curRommData.rightcDoor)
                    {
                        int ranY = Random.Range(curRommData.center.y - height / 2 + 2, curRommData.center.y + height / 2 - 2);
                        Vector2Int pot = new Vector2Int(curRommData.center.x + (width / 2), ranY);
                        curRommData.rightDoorPoint = pot;
                    }


                    curRommData.rightDoorObj = Instantiate(curRommData.roomData.rightDoorObj, (Vector2)curRommData.rightDoorPoint + Vector2.right * 0.5f, Quaternion.identity);
                    GameObject tileDoor = Instantiate(curRommData.roomData.tile_rightDoor, (Vector2)curRommData.rightDoorPoint + Vector2.right * 3, Quaternion.identity);

                    curRommData.rightDoor = true;
                    nextRoom.leftcDoor = true;
                    nextRoom.leftDoorPoint = curRommData.rightDoorPoint;
                    nextRoom.leftDoorPoint.x = nextRoom.center.x - nextRoom.width / 2;

                    //todo
                    curRommData.rightDoorObj.GetComponent<Door>().SetData(nextRoom, num, curRommData.roomNumber, tileDoor, (Vector2)curRommData.rightDoorPoint + Vector2.right * 3);
                    curRommData.rightDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    curRommData.doors.Add(curRommData.rightDoorObj.GetComponent<Door>());



                }
                break;
            case 1: //T
                if (!curRommData.topDoor)
                {
                    if (!curRommData.topcDoor)
                    {
                        int ranX = Random.Range(curRommData.center.x - width / 2 + 2, curRommData.center.x + width / 2 - 2);
                        Vector2Int pot = new Vector2Int(ranX, curRommData.center.y + (height / 2));
                        curRommData.topDoorPoint = pot;
                    }


                    curRommData.topDoorObj = Instantiate(curRommData.roomData.topDoorObj, (Vector2)curRommData.topDoorPoint, Quaternion.identity);
                    GameObject tileDoor = Instantiate(curRommData.roomData.tile_topDoor, (Vector2)curRommData.topDoorPoint, Quaternion.identity);


                    curRommData.topDoor = true;
                    nextRoom.bottomcDoor = true;

                    nextRoom.bottomDoorPoint = curRommData.topDoorPoint;
                    nextRoom.bottomDoorPoint.y = nextRoom.center.y - nextRoom.height / 2;

                    curRommData.topDoorObj.GetComponent<Door>().SetData(nextRoom, num, curRommData.roomNumber, tileDoor, (Vector2)curRommData.topDoorPoint);
                    curRommData.topDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    curRommData.doors.Add(curRommData.topDoorObj.GetComponent<Door>());


                }

                break;
            case 2: //L
                if (!curRommData.leftDoor)
                {

                    curRommData.leftDoorObj = Instantiate(curRommData.roomData.leftDoorObj, (Vector2)curRommData.leftDoorPoint + Vector2.right * 0.5f, Quaternion.identity);
                    GameObject tileDoor = Instantiate(curRommData.roomData.tile_leftDoor, (Vector2)curRommData.leftDoorPoint + Vector2.right * 3, Quaternion.identity);

                    curRommData.leftDoor = true;
                    nextRoom.rightcDoor = true;
                    curRommData.leftDoorObj.GetComponent<Door>().SetData(nextRoom, num, curRommData.roomNumber, tileDoor, (Vector2)curRommData.leftDoorPoint + Vector2.right * 3);
                    curRommData.leftDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    curRommData.doors.Add(curRommData.leftDoorObj.GetComponent<Door>());


                }

                break;
            case 3: //B
                if (!curRommData.bottomDoor)
                {
                    curRommData.bottomDoorObj = Instantiate(curRommData.roomData.bottomDoorObj, (Vector2)curRommData.bottomDoorPoint, Quaternion.identity);
                    GameObject tileDoor = Instantiate(curRommData.roomData.tile_bottomDoor, (Vector2)curRommData.bottomDoorPoint + Vector2.up, Quaternion.identity);


                    curRommData.bottomDoor = true;

                    nextRoom.topcDoor = true;
                    curRommData.bottomDoorObj.GetComponent<Door>().SetData(nextRoom, num, curRommData.roomNumber, tileDoor, (Vector2)curRommData.bottomDoorPoint + Vector2.up);
                    curRommData.bottomDoorObj.transform.SetParent(container.transform);
                    tileDoor.transform.SetParent(container.transform);
                    curRommData.doors.Add(curRommData.bottomDoorObj.GetComponent<Door>());


                }

                break;
        }


    }

    #endregion

    #region MiniMap-------------------------------------------------------------------------------------------------

    public void CreateMinimap()
    {
        foreach(RoomData roomData in DunGoenManager.Instance.dungoenRoomDataList)
        {
            Transform container;

            if (!DunGoenManager.Instance.container.transform.Find($"Room {roomData.roomNumber}"))
            {
                container = new GameObject($"Room {roomData.roomNumber}").transform;
            }
            else
            {
                container = DunGoenManager.Instance.container.transform.Find($"Room {roomData.roomNumber}");
            }


            GameObject spriteObj = Instantiate(minimapSpriteObj, (Vector2)roomData.center, Quaternion.identity);
            spriteObj.transform.SetParent(container);

            DunGoenManager.Instance.minimapSpriteList.Add(spriteObj);

        }

        CreateMinimapLine();
    }


    void CreateMinimapLine()
    {
        foreach (RoomData roomData in DunGoenManager.Instance.dungoenRoomDataList)
        {
            for (int i = 0; i < dir.Count; i++)
            {
                string dirString = "";
                Vector2Int checkNextRoomPoint = dir[i];
                checkNextRoomPoint *= new Vector2Int(roomData.width + 10, roomData.height + 10);
                checkNextRoomPoint = roomData.center + checkNextRoomPoint;
                if (path.Contains(checkNextRoomPoint))
                {
                    RoomData nextRoomData = FindRoomdata(checkNextRoomPoint);

                    Vector2Int point = (nextRoomData.center- roomData.center) /2;

                    if (point.x == 0) dirString = "col";
                    else if(point.y == 0)dirString = "row";                   
                    point = roomData.center + point;
                    minimapLineList.Add(new LinePath(dirString, point));

                }
            }
            
        }

        foreach(LinePath l in minimapLineList)
        {

            switch (l.dir)
            {
                case "row":
                    GameObject newLine = Instantiate(rowLine,(Vector2)l.point, Quaternion.identity);
                    newLine.transform.SetParent(DunGoenManager.Instance.container.transform);
                    break;
                case "col":
                    GameObject newLine1 = Instantiate(colLine, (Vector2)l.point, Quaternion.identity);
                    newLine1.transform.SetParent(DunGoenManager.Instance.container.transform);
                    break;
            }
        }

    }


    #endregion



    void DungoenuAllDoorOff()
    {
        foreach(RoomData room in DunGoenManager.Instance.dungoenRoomDataList)
        {
            room.AllDoorOff();
        }
    }


    RoomData FindRoomdata(Vector2Int point)
    {
        foreach(RoomData roomData in DunGoenManager.Instance.dungoenRoomDataList)
        {
            if(roomData.center == point)
            {
                return roomData;
            }
        }
        return null;
    }

    Vector2Int GetCreatePoint(RoomData roomData)
    {
        Vector2Int ranDir = dir[Random.Range(0, dir.Count)];
        ranDir *= new Vector2Int(roomData.width+offset,roomData.height+offset);

        return ranDir;

    }



}



public struct LinePath
{
    public string dir;
    public Vector2Int point;

    public LinePath(string _dir, Vector2Int _point)
    {
        dir = _dir;
        point = _point;
    }
}