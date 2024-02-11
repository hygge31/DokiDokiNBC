using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DungoenGenerator : MonoBehaviour
{
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
        DunGoenManager.Instance.tileDrawer.DrawFloorTiles();
        DunGoenManager.Instance.tileDrawer.DrawWallTile();
        CreateDoor();
    }


    public List<Vector2Int> RandomCreateRoomPosition(Vector2Int startPoint,int maxRoomCount)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        DunGoenManager.Instance.dungoenRoomDataList.Clear();

        Vector2Int curPoint = startPoint;
        // -- Init
        path.Add(curPoint);
        RoomData curRoomData = new RoomData(roomDataSOs[Random.Range(0,roomDataSOs.Count)]);
        curRoomData.SetRoomData(curPoint,0);
        DunGoenManager.Instance.dungoenRoomDataList.Add(curRoomData);
        CreateCollider(curRoomData, curPoint);
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
            CreateCollider(curRoomData, curPoint);
            DunGoenManager.Instance.dungoenRoomDataList.Add(curRoomData);
        }
        return path;
    }

    void CreateCollider(RoomData room, Vector2Int point)
    {
        GameObject newColliderBox = new GameObject("Collider");
        newColliderBox.transform.position = (Vector2)point;
        newColliderBox.AddComponent<BoxCollider2D>().size = new Vector2(room.width, room.height);
        newColliderBox.AddComponent<Rigidbody2D>().gravityScale = 0;
        newColliderBox.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        newColliderBox.GetComponent<BoxCollider2D>().isTrigger = true;
        newColliderBox.layer = 6;
        
        newColliderBox.transform.SetParent(DunGoenManager.Instance.container);
        DunGoenManager.Instance.colliderList.Add(newColliderBox);
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
