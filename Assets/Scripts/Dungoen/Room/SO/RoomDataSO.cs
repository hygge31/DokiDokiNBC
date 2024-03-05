using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Create Room Data",menuName = "Default room data")]
public class RoomDataSO : ScriptableObject
{
    public int width;
    public int height;

    public GameObject[] doorObjs = new GameObject[4];
    public GameObject[] tileDoorObjs = new GameObject[4];



}
