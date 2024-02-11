using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTemp : MonoBehaviour
{
    private void Start()
    {
        Invoke("TestItemDrop", 3f);
    }

    private void TestItemDrop()
    {
        StartCoroutine(MultiDrop());
    }

    private IEnumerator MultiDrop()
    {
        for (int i = 0; i < 10; i++)
        {
            Item_CodePiece item = Managers.RM.Instantiate("Items/Item_CodePiece").GetComponent<Item_CodePiece>();
            item.Setup();            
            yield return new WaitForSeconds(0.2f);
        }

        Destroy(gameObject);
    }
}
