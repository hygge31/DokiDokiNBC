using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
    public CharacterStats CurrentStates { get; private set; }

    private void Awake()
    {
        UpdateCharacterStats();
    }

    private void UpdateCharacterStats()
    {
        CurrentStates = new CharacterStats { };
        CurrentStates.statsChangeType = baseStats.statsChangeType;
        CurrentStates.maxHealth = baseStats.maxHealth;
        CurrentStates.speed = baseStats.speed;

    }
}
