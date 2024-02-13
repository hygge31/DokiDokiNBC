using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData",menuName = "Monster/MonsterData",order = 0) ]
public class EnemySO : ScriptableObject
{
    [Header("Health Info")]
    public float hp;

    [Header("Attck Info")]
    public float power;
    public float delay;
}
