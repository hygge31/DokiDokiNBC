using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public int curRoomNumber;
    public int nextRoomNumber;
    public Vector2 originPosition;
    Vector2 appearanvePot;
    public Vector2 outPoint;


    float speed = 0.1f;

    public SpriteRenderer spriteRenderer;

    public BoxCollider2D _collider;

    public GameObject tile_door;


    public Color orginColor;
    public Color targetColor;
    public Color curColor;

    public ParticleSystem smokeEffect;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }



    public void SetData(RoomData room, int num, int curRoomNumber,GameObject tileDoor,Vector2 orgPot)
    {
        
        nextRoomNumber = room.roomNumber;
        tile_door = tileDoor;


        orginColor = tile_door.transform.GetChild(0).GetComponent<Tilemap>().color;
        Color newColor = orginColor;
        newColor.a = 0;
        targetColor = newColor;


        this.curRoomNumber = curRoomNumber;
        originPosition = orgPot;
        appearanvePot = originPosition + Vector2.up * 100;
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





    public void AppearanceDoor()
    {
        StartCoroutine(AppearanceDoorCo());
        smokeEffect.Play();
        LerpCor(1);
        
    }

    IEnumerator AppearanceDoorCo()
    {
        float percent = 0;
        tile_door.SetActive(true);
        while (percent < 1)
        {
            percent += Time.deltaTime;
            tile_door.transform.position = Vector3.Lerp(tile_door.transform.position, originPosition, percent);
            yield return null;
        }
        tile_door.transform.position = originPosition;
        
        _collider.enabled = true;
    }


    public void ExitDoor()
    {
        _collider.enabled = false;
        LerpCor(0);
        StartCoroutine(ExitDoorCo());


    }

    IEnumerator ExitDoorCo()
    {
        float percent = 0;

        while(percent < 1)
        {
            percent += Time.deltaTime * 0.5f;
            tile_door.transform.position = Vector3.Lerp(tile_door.transform.position, appearanvePot, percent);
            yield return null;
        }
        tile_door.transform.position = appearanvePot;

        tile_door.SetActive(false);
    }




    public void LerpCor(int num)
    {
        foreach(Transform t in tile_door.transform)
        {
            Tilemap tilemap = t.GetComponent<Tilemap>();
            StartCoroutine(LerpCorCo(tilemap, num));

        }
    }

    IEnumerator LerpCorCo(Tilemap tilemap,int num)
    {
        float percent = 0;


        if(num == 0) 
        {
            while (percent < 1)
            {
                percent += Time.deltaTime;
                tilemap.color = Color.Lerp(curColor, targetColor, percent);
                yield return null;

            }
            curColor = targetColor;
        }
        else
        {
            while (percent < 1)
            {
                percent += Time.deltaTime;
                tilemap.color = Color.Lerp(curColor, orginColor, percent);
                yield return null;

            }
            curColor = orginColor;
        }

       

    }


  


    public void DoorOff()
    {
        _collider.enabled = false;
        tile_door.transform.position = appearanvePot;
        tile_door.SetActive(false);
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
