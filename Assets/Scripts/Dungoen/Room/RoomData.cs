using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[System.Serializable]
public class RoomData
{
    //Room SO
    public int roomNumber;
    public int width = 30; //todo
    public int height = 30; //todo

    public Vector2Int center;
    public BoundsInt bounds;

    public Vector2Int leftDoorPoint;
    public Vector2Int rightDoorPoint;
    public Vector2Int upDoorPoint;
    public Vector2Int downDoorPoint;

    public GameObject minimapSprite;//todo 

   
    public void SetRoomData(Vector2Int createPoint,int roomNumber)
    {
        center = createPoint;
        this.roomNumber = roomNumber;
        bounds = new BoundsInt(new Vector3Int(center.x - (width / 2), center.y - (height / 2), 0), new Vector3Int(width, height, 0));

        leftDoorPoint = new Vector2Int(center.x - (width / 2), center.y);
        rightDoorPoint = new Vector2Int(center.x + (width / 2), center.y);
        upDoorPoint = new Vector2Int(center.x, center.y + (height / 2));
        downDoorPoint = new Vector2Int(center.x, center.y - (height / 2));
    }
}
