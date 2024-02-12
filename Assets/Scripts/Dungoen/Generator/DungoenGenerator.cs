using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        //Create minimap
        CreateMinimap();
        CreateMinimapLine();
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
                    roomData.CreateDoor(nextRoomData, i);
                }
            }
            //roomData.ToggleDoor();
        }
    }

    #region MiniMap-------------------------------------------------------------------------------------------------

    void CreateMinimap()
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
        ranDir *= new Vector2Int(roomData.width+10,roomData.height+10);

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