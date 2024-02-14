using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats CurrentStates { get; private set; }

    public List<CharacterStats> statsModifiers = new List<CharacterStats>();

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        CurrentStates = new CharacterStats { };
        CurrentStates.maxHealth = baseStats.maxHealth;
        CurrentStates.moveSpeed = baseStats.moveSpeed;
        CurrentStates.fireRate = baseStats.fireRate;
        CurrentStates.atk = baseStats.atk;
    }
}
