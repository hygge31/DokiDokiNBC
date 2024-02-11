using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public RoomData nextRoomData;
    public Vector2Int outPoint;





    public void SetData(RoomData room, int num)
    {
        nextRoomData = room;

        switch (num) //RTLB
        {
            case 0:
                outPoint = room.leftDoorPoint + new Vector2Int(1,0);
                break;
            case 1:
                outPoint = room.bottomDoorPoint + new Vector2Int(0, 1);
                break;
            case 2:
                outPoint = room.rightDoorPoint + new Vector2Int(-1, 0);
                break;
            case 3:
                outPoint = room.topDoorPoint + new Vector2Int(0, -1);
                break;

        }
        
    }

    void TransformPlayer(GameObject player)
    {
        player.transform.position = (Vector2)outPoint;
        DunGoenManager.Instance.curDungoenRoomNumber = nextRoomData.roomNumber;
        nextRoomData.SpawnMonster();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player");
        }
    }
}
