using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int curRoomNumber;
    public int nextRoomNumber;
    public Vector3 originPosition;
    public Vector2Int outPoint;

    public SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    public void SetData(RoomData room, int num, int curRoomNumber)
    {
        nextRoomNumber = room.roomNumber;
        this.curRoomNumber = curRoomNumber;
        originPosition = gameObject.transform.position;
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



    //public void AppearanceDoor()
    //{
        
    //    gameObject.transform.position += Vector3.up * 20;
    //    StartCoroutine(AppearanceDoorCo());


    //}

    //IEnumerator AppearanceDoorCo()
    //{
    //    float percent = 0;
       
    //    while (percent < 1)
    //    {
    //        percent += Time.deltaTime * 0.01f;
    //        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, originPosition, percent);

    //        yield return null;
    //    }
    //}
    

    public void ExitDoor()
    {
        
    }

    


    void TransformPlayer(GameObject player)
    {
        //Coroutine flash display 0.5f
        player.transform.position = (Vector2)outPoint;
        //todo
        DunGoenManager.Instance.curDungoenRoomNumber = nextRoomNumber;
        
        DunGoenManager.Instance.dungoenRoomDataList[nextRoomNumber].SpawnMonster();
        //todo

        DunGoenManager.Instance.minimapSpriteList[curRoomNumber].GetComponent<MinimapSprite>().OutPoisition();
        DunGoenManager.Instance.minimapSpriteList[nextRoomNumber].GetComponent<MinimapSprite>().CurPosition();

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TransformPlayer(collision.gameObject);
        }
    }
}
