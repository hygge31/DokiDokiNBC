using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public RoomDataSO roomData;
    public GameObject boosObj;
    public GameObject playerObj;

    public Transform spawnPot;
    public Transform playerSpawnPot;

    
    private void Start()
    {
        StartCoroutine(BossAppearCo());
    }

    IEnumerator BossAppearCo()
    {
        //GameObject boss = Instantiate(boosObj, spawnPot.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        
    }

}
