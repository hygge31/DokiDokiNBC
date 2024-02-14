using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisappear : MonoBehaviour
{
    public void Disappear()
    {
        //foreach (Behaviour component in transform.parent.GetComponentsInChildren<Behaviour>())
        //{
        //    component.enabled = false;
        //}
        DunGoenManager.Instance.DieMonster();
        Destroy(transform.parent.gameObject);
    }
}
