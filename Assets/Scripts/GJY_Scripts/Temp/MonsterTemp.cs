using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterTemp : MonoBehaviour
{
    private void Start()
    {
        //Invoke("TestItemDrop", 3f);
    }

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
    }

    private void TestItemDrop()
    {
        StartCoroutine(MultiDrop());
    }

    private IEnumerator MultiDrop()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return null;
        }

        Destroy(gameObject);
    }
}
