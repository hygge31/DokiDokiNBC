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


    [Header("Portal")]
    public GameObject portal;
    public GameObject spawner;

    public void ProcedurealDungoenGenerator()
    {
        path = RandomCreateRoomPosition(Vector2Int.zero, maxRoomCount); //todo fix player position,

        tileDrawer.DrawAllTile();

        CreateDoor();
        DungoenuAllDoorOff();

        RandomPortalPoint();
        RandomSpawnerPosition();

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
        curRoomData.clear = true;
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


    void RandomPortalPoint()
    {
        int ran = Random.Range(1, DunGoenManager.Instance.dungoenRoomDataList.Count);
        Debug.Log(ran);
        DunGoenManager.Instance.dungoenRoomDataList[ran].dungoenType = DunGoneType.Portal;
        Transform contain = DunGoenManager.Instance.container.transform.Find($"Room {ran}");

        GameObject newPortal = Instantiate(portal, (Vector2)DunGoenManager.Instance.dungoenRoomDataList[ran].center, Quaternion.identity);
        newPortal.transform.SetParent(contain);
    }


    void RandomSpawnerPosition()
    {
        foreach(RoomData roomData in DunGoenManager.Instance.dungoenRoomDataList)
        {
            Transform contain = DunGoenManager.Instance.container.transform.Find($"Room {roomData.roomNumber}");

            //init
            int maxSpawn = Random.Range(Managers.GameManager.day,2 + Managers.GameManager.day);
            //
            for (int i = 0; i < maxSpawn; i++)
            {
                Vector2Int minLimit = (Vector2Int)roomData.bounds.min + new Vector2Int(4, 4);
                Vector2Int maxLimit = (Vector2Int)roomData.bounds.max - new Vector2Int(4, 4);

                int ranx = Random.Range(minLimit.x, maxLimit.x);
                int rany = Random.Range(minLimit.y, maxLimit.y);

                GameObject newSpawner = Instantiate(spawner, new Vector2(ranx, rany), Quaternion.identity);
                roomData.spawnerList.Add(newSpawner.GetComponent<Spawner>());

                newSpawner.transform.SetParent(contain);
            }
            roomData.clearCondition = maxSpawn;

           
        }

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


    public void CreateDoor(RoomData curRommData, RoomData nextRoom, int num) ////040229 Test Code
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

                if (!curRommData.existDoors[0])
                {
                    if (!curRommData.existedDoors[0])
                    {
                        int ranY = Random.Range(curRommData.center.y - height / 2 + 2, curRommData.center.y + height / 2 - 2);
                        Vector2Int pot = new Vector2Int(curRommData.center.x + (width / 2), ranY);
                        curRommData.doorPoints[num] = pot;
                    }

                    nextRoom.doorPoints[2] = curRommData.doorPoints[0];
                    nextRoom.doorPoints[2].x = nextRoom.center.x - nextRoom.width / 2;

                    CreateDoorAndInitData(container, curRommData, nextRoom, num, 2);

                }
                break;
            case 1: //T
                if (!curRommData.existDoors[1])
                {
                    if (!curRommData.existedDoors[1])
                    {
                        int ranX = Random.Range(curRommData.center.x - width / 2 + 2, curRommData.center.x + width / 2 - 2);
                        Vector2Int pot = new Vector2Int(ranX, curRommData.center.y + (height / 2));
                        curRommData.doorPoints[num] = pot;
                    }

                    nextRoom.doorPoints[3] = curRommData.doorPoints[1];
                    nextRoom.doorPoints[3].y = nextRoom.center.y - nextRoom.height / 2;

                    CreateDoorAndInitData(container, curRommData, nextRoom, num, 3);
                }

                break;
            case 2: //L
                if (!curRommData.existDoors[2])
                {
                    CreateDoorAndInitData(container, curRommData, nextRoom, num, 0);
                }

                break;
            case 3: //B
                if (!curRommData.existDoors[3])
                {
                    CreateDoorAndInitData(container, curRommData, nextRoom, num, 1);
                }

                break;
        }
    }
    //RTLB , 
    void CreateDoorAndInitData(Transform container, RoomData curRoomData, RoomData nextRoom, int num, int opposite)
    {
        curRoomData.doorObjs[num] = Instantiate(curRoomData.roomData.doorObjs[num], (Vector2)curRoomData.doorPoints[num], Quaternion.identity);
        GameObject tileDoor = Instantiate(curRoomData.roomData.tileDoorObjs[num], (Vector2)curRoomData.doorPoints[num], Quaternion.identity);

        curRoomData.existDoors[num] = true;
        nextRoom.existedDoors[opposite] = true;
        curRoomData.doorObjs[num].GetComponent<Door>().SetData(nextRoom, num, curRoomData.roomNumber, tileDoor, curRoomData.doorPoints[num]);

        curRoomData.doorObjs[num].transform.SetParent(container);
        tileDoor.transform.SetParent(container);
        curRoomData.doors.Add(curRoomData.doorObjs[num].GetComponent<Door>());
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
            spriteObj.GetComponent<MinimapSprite>().dunGoneType = roomData.dungoenType;

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