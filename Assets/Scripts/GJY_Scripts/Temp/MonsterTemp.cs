using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterTemp : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Item_CodePiece item = Managers.RM.Instantiate("Items/Item_CodePiece").GetComponent<Item_CodePiece>();
            item.Setup(transform);            
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            Item_Health item = Managers.RM.Instantiate("Items/Item_Health").GetComponent<Item_Health>();
            item.Setup(transform);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            string[] perkName = Enum.GetNames(typeof(Define.Perks));
            Item_SO itemSO = Resources.Load<Item_SO>($"Scriptable/{perkName[Random.Range(0, perkName.Length)]}");

            Item_Perk item = Managers.RM.Instantiate($"Items/Item_Perk").GetComponent<Item_Perk>();
            item.Setup(itemSO, transform);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            string[] itemNames = Enum.GetNames(typeof(Define.Weapons));
            Item_SO itemSO = Resources.Load<Item_SO>($"Scriptable/{itemNames[Random.Range(0, itemNames.Length)]}");

            Item_Weapon weapon = Managers.RM.Instantiate($"Items/Item_Weapon").GetComponent<Item_Weapon>();
            weapon.Setup(itemSO, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Managers.Player.GetDamaged();
    }
}
