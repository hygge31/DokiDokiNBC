using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    [Range(1, 20)] public int maxHealth;
    [Range(0f, 20f)] public float atk;
    [Range(0f, 3f)] public float fireRate;
    [Range(0f, 10f)] public float moveSpeed;
}
