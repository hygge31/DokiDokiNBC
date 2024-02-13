using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    protected Animator animator;
    protected EnemyController enemyController;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
    }
}
