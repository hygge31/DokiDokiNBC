using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Vector2 direction;
    private void Awake()
    {
    }
    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        //agent.updateRotation = false;
        agent.updateUpAxis = false;
        Invoke("fly", 0.2f);
    }
    private void Update()
    {
        agent.SetDestination(target.position);
        Rotate();
    }
    public void Rotate()
    {
        GetDirection();
        Vector3 vector = new Vector3(0, 0, 0);
        agent.transform.eulerAngles = vector;
        int num = direction.x < 0 ? -1 : 1;
        transform.localScale = new Vector3(num,1,1);
    }
    public void GetDirection()
    {
        direction = (target.position - transform.position).normalized;
    }
    public void fly(bool isUp)
    {
        this.agent.velocity += isUp == true ? Vector3.up * 2f:Vector3.down* 2f;
        Invoke("fly", 0.2f);
    }
}
