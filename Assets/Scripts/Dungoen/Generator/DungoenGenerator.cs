using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungoenGenerator : MonoBehaviour
{
    public int maxRoomCount = 12;
    
    

    public List<Vector2Int> list = new List<Vector2Int>();


     List<Vector2Int> dir = new List<Vector2Int>()
    {
        new Vector2Int(1,0),    //R
        new Vector2Int(0,1),    //U
        new Vector2Int(-1,0),   //L
        new Vector2Int(0,-1),   //D
    };


    public void ProcedurealDungoenGenerator()
    {
        RandomCreateRoomPosition(Vector2Int.zero, maxRoomCount); //todo fix player position,
        DunGoenManager.Instance.tileDrawer.DrawFloorTiles();
        DunGoenManager.Instance.tileDrawer.DrawWallTile();

    }


    public void RandomCreateRoomPosition(Vector2Int startPoint,int maxRoomCount)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        DunGoenManager.Instance.dungoenRoomDataList.Clear();

        Vector2Int curPoint = startPoint;
        // -- Init
        path.Add(curPoint);
        RoomData startRoomData = new RoomData();
        startRoomData.SetRoomData(curPoint,0);
        DunGoenManager.Instance.dungoenRoomDataList.Add(startRoomData);
        // -- Init

        for (int i = 1; i < maxRoomCount; i++)
        {
            RoomData roomData = new RoomData();
            curPoint += GetCreatePoint(roomData);

            while (path.Contains(curPoint))
            {
                curPoint += GetCreatePoint(roomData);
            }

            path.Add(curPoint);
            roomData.SetRoomData(curPoint,i);
            DunGoenManager.Instance.dungoenRoomDataList.Add(roomData);
        }

    }



    Vector2Int GetCreatePoint(RoomData roomData)
    {
        Vector2Int ranDir = dir[Random.Range(0, dir.Count)];
        ranDir *= new Vector2Int(roomData.width+10,roomData.height+10);

        return ranDir;

    }



}
