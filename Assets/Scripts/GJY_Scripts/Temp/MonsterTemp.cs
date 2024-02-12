using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
