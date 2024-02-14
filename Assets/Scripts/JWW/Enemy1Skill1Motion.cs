using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Skill1Motion : MonoBehaviour
{
    public GameObject Skill1;
    private Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
