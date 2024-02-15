using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropManager
{
    private readonly float _healthPercentage = 75f;
    private readonly float _perkPercentage = 70f;
    private readonly float _weaponPercentage = 50f;    

    public void DropCodePiece(Transform dropPos)
    {
        Item_CodePiece item = Managers.RM.Instantiate("Items/Item_CodePiece").GetComponent<Item_CodePiece>();
        item.Setup(dropPos);
    }

    public void DropHealthRandom(Transform dropPos)
    {
        if (RandomGet(_healthPercentage) == false)
            return;

        Item_Health item = Managers.RM.Instantiate("Items/Item_Health").GetComponent<Item_Health>();
        item.Setup(dropPos);
    }

    public void DropPerk(Transform dropPos)
    {
        if (RandomGet(_perkPercentage) == false)
            return;

        string[] perkName = Enum.GetNames(typeof(Define.Perks));
        Item_SO itemSO = Resources.Load<Item_SO>($"Scriptable/{perkName[Random.Range(0, perkName.Length)]}");

        Item_Perk item = Managers.RM.Instantiate($"Items/Item_Perk").GetComponent<Item_Perk>();
        item.Setup(itemSO, dropPos);
    }

    public void DropWeapon(Transform dropPos)
    {
        if (RandomGet(_weaponPercentage) == false)
            return;
        
        string[] itemNames = Enum.GetNames(typeof(Define.Weapons));
        Item_SO itemSO = Resources.Load<Item_SO>($"Scriptable/{itemNames[Random.Range(1, itemNames.Length)]}");

        Item_Weapon weapon = Managers.RM.Instantiate($"Items/Item_Weapon").GetComponent<Item_Weapon>();
        weapon.Setup(itemSO, dropPos);
    }

    private bool RandomGet(float percentageType)
    {
        float random = Random.Range(0f, 100f);

        return random < percentageType;
    }
}
