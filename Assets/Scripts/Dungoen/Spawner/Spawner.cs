using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> monsterList = new List<GameObject>();


    public void Spawn()
    {
        int ran = Random.Range(0, monsterList.Count);

        Instantiate(monsterList[ran],transform.position,Quaternion.identity);
    }
}
