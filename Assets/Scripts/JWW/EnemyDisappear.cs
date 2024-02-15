using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappear : MonoBehaviour
{
    WaitForSeconds wait = new WaitForSeconds(0.1f);

    public void Disappear()
    {
        foreach (SpriteRenderer component in transform.parent.GetComponentsInChildren<SpriteRenderer>())
        {
            component.enabled = false;
        }
        StartCoroutine(DropRoutine());        
    }

    private IEnumerator DropRoutine()
    {
        int randomCount = Random.Range(5, 16);

        for (int i = 0; i < randomCount; i++)
        {
            Managers.Drop.DropCodePiece(transform);
            yield return wait;
        }
        Managers.Drop.DropHealthRandom(transform);
        Managers.Drop.DropPerk(transform);

        DunGoenManager.Instance.DieMonster();
        Destroy(transform.parent.gameObject);
    }
}
