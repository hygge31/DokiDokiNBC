using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject wall;
    public GameObject door;
    SpriteRenderer spriteRenderer;

    public int size;

    public int leftDoor;
    public int rightDoor;
    public int upDoor;
    public int downDoor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        size = (int)spriteRenderer.bounds.size.x;
    }


    private void Start()
    {
        SetWall();
    }

    void SetWall()
    {
        GameObject leftWall = Instantiate(wall, transform);
        leftWall.transform.position = transform.position - new Vector3(size / 2, 0);
        leftWall.transform.localScale = new Vector3(1, size, 1);
        GameObject rightWall = Instantiate(wall, transform);
        rightWall.transform.position = transform.position + new Vector3(size / 2, 0);
        rightWall.transform.localScale = new Vector3(1, size, 1);
        GameObject upWall = Instantiate(wall, transform);
        upWall.transform.position = transform.position + new Vector3(0 , size / 2);
        upWall.transform.localScale = new Vector3(size, 1, 1);
        GameObject downWall = Instantiate(wall, transform);
        downWall.transform.position = transform.position - new Vector3(0, size / 2);
        downWall.transform.localScale = new Vector3(size, 1, 1);



        if(leftDoor == 1)
        {
            GameObject newDoor = Instantiate(door, transform);
            newDoor.transform.position = transform.position - new Vector3(size / 2, 0, 0);
        }
        if (rightDoor == 1)
        {
            GameObject newDoor = Instantiate(door, transform);
            newDoor.transform.position = transform.position + new Vector3(size / 2, 0, 0);
        }
        if (upDoor == 1)
        {
            GameObject newDoor = Instantiate(door, transform);
            newDoor.transform.position = transform.position + new Vector3(0, size / 2, 0);
        }
        if (downDoor == 1)
        {
            GameObject newDoor = Instantiate(door, transform);
            newDoor.transform.position = transform.position - new Vector3(0, size / 2, 0);
        }

    }
}
